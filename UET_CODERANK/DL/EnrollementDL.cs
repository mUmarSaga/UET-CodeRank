using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using UET_CODERANK.Model;

namespace UET_CODERANK.DL
{
    public class EnrollmentDL
    {
        public static bool SendRequest(int studentId, int sectionId)
        {
            string query = "INSERT INTO enrollment_request (student_id, section_id, status, requested_at) " +
                           "VALUES (@studentId, @sectionId, 'PENDING', @requestedAt)";
            MySqlParameter[] p = {
                new MySqlParameter("@studentId", studentId),
                new MySqlParameter("@sectionId", sectionId),
                new MySqlParameter("@requestedAt", DateTime.Now)
            };
            return DatabaseHelper.ExecuteNonQuery(query, p) > 0;
        }

        public static EnrollementRequest GetLatestRequest(int studentId)
        {
            string query = "SELECT * FROM enrollment_request WHERE student_id = @studentId " +
                           "ORDER BY requested_at DESC LIMIT 1";
            MySqlParameter[] p = { new MySqlParameter("@studentId", studentId) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, p);

            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new EnrollementRequest
            {
                Id = Convert.ToInt32(row["id"]),
                StudentId = Convert.ToInt32(row["student_id"]),
                SectionId = Convert.ToInt32(row["section_id"]),
                Status = Enum.Parse<EnrollementStatus>(row["status"].ToString()),
                RequestedAt = Convert.ToDateTime(row["requested_at"]),
                ReviewedAt = row["reviewed_at"] == DBNull.Value ? null : Convert.ToDateTime(row["reviewed_at"])
            };
        }

        public static bool HasPendingRequest(int studentId)
        {
            string query = "SELECT COUNT(*) FROM enrollment_request WHERE student_id = @studentId AND status = 'PENDING'";
            MySqlParameter[] p = { new MySqlParameter("@studentId", studentId) };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p)) > 0;
        }

        public static bool ApproveRequest(int requestId, int studentId, int sectionId)
        {
            try
            {
                // Update request status
                string q1 = "UPDATE enrollment_request SET status = 'APPROVED', reviewed_at = @reviewedAt WHERE id = @id";
                MySqlParameter[] p1 = {
                    new MySqlParameter("@reviewedAt", DateTime.Now),
                    new MySqlParameter("@id", requestId)
                };
                DatabaseHelper.ExecuteNonQuery(q1, p1);

                // Update student section
                string q2 = "UPDATE student SET section_id = @sectionId, is_approved = 1 WHERE id = @studentId";
                MySqlParameter[] p2 = {
                    new MySqlParameter("@sectionId", sectionId),
                    new MySqlParameter("@studentId", studentId)
                };
                DatabaseHelper.ExecuteNonQuery(q2, p2);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool RejectRequest(int requestId)
        {
            string query = "UPDATE enrollment_request SET status = 'REJECTED', reviewed_at = @reviewedAt WHERE id = @id";
            MySqlParameter[] p = {
                new MySqlParameter("@reviewedAt", DateTime.Now),
                new MySqlParameter("@id", requestId)
            };
            return DatabaseHelper.ExecuteNonQuery(query, p) > 0;
        }

        public static List<EnrollementRequest> GetAllPending()
        {
            string query = "SELECT * FROM enrollment_request WHERE status = 'PENDING' ORDER BY requested_at ASC";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            List<EnrollementRequest> list = new List<EnrollementRequest>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new EnrollementRequest
                {
                    Id = Convert.ToInt32(row["id"]),
                    StudentId = Convert.ToInt32(row["student_id"]),
                    SectionId = Convert.ToInt32(row["section_id"]),
                    Status = Enum.Parse<EnrollementStatus>(row["status"].ToString()),
                    RequestedAt = Convert.ToDateTime(row["requested_at"]),
                    ReviewedAt = row["reviewed_at"] == DBNull.Value ? null : Convert.ToDateTime(row["reviewed_at"])
                });
            }
            return list;
        }
    }
}