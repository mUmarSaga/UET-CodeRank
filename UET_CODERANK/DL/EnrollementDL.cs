using System;
using System.Collections.Generic;
using System.Data;

using MySql.Data.MySqlClient;

namespace UET_CODERANK.DL
{
    internal class EnrollementDL
    {
        public static void AddRequest(Model.EnrollementRequest request)
        {
            string querry = $"INSERT INTO enrollement_request (id,student_id,section_id,status,requested_at,reviewed_at) VALUES (@id,@student_id,@section_id,@status,@requested_at,@reviewed_At)";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@id", request.Id),
                new MySqlParameter("@student_id", request.StudentId),
                new MySqlParameter("@section_id", request.SectionId),
                new MySqlParameter("@status", request.Status.ToString()),
                new MySqlParameter("@requested_at", request.RequestedAt),
                new MySqlParameter("@reviewed_at", request.ReviewedAt)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);

            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static List<Model.EnrollementRequest> GetRequestsByStudentId(int studentId)
        {
            string querry = $"SELECT * FROM enrollement_request WHERE student_id = @student_id";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@student_id", studentId)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                List<Model.EnrollementRequest> requests = new List<Model.EnrollementRequest>();
                foreach (DataRow row in dt.Rows)
                {
                    requests.Add(new Model.EnrollementRequest
                    {
                        Id = Convert.ToInt32(row["id"]),
                        StudentId = Convert.ToInt32(row["student_id"]),
                        SectionId = Convert.ToInt32(row["section_id"]),
                        Status = Enum.Parse<Model.EnrollementStatus>(row["status"].ToString()),
                        RequestedAt = Convert.ToDateTime(row["requested_at"]),
                        ReviewedAt = row["reviewed_at"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["reviewed_at"])
                    });
                }
                return requests;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static List<Model.EnrollementRequest> GetPending()
        {
            string querry  = $"SELECT * FROM enrollement_request WHERE status = @status";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@status", "PENDING")
            };
            try
            {
                DataTable table = DatabaseHelper.ExecuteQuery(querry, parameters);
                List<Model.EnrollementRequest> requests = new List<Model.EnrollementRequest>();
                foreach (DataRow row in table.Rows) { 
                    requests.Add(new Model.EnrollementRequest
                    {
                        Id = Convert.ToInt32(row["id"]),
                        StudentId = Convert.ToInt32(row["student_id"]),
                        SectionId = Convert.ToInt32(row["section_id"]),
                        Status = Enum.Parse<Model.EnrollementStatus>(row["status"].ToString()),
                        RequestedAt = Convert.ToDateTime(row["requested_at"]),
                        ReviewedAt = row["reviewed_at"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["reviewed_at"])
                    });
                }
                return requests;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static void AcceptRequest(int StudentID)
        {
            string querry = $"UPDATE enrollement_request SET status = @status, reviewed_at = @reviewed_at WHERE student_id = @student_id AND status = @pending_status";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@status", "ACCEPTED"),
                new MySqlParameter("@reviewed_at", DateTime.Now),
                new MySqlParameter("@student_id", StudentID),
                new MySqlParameter("@pending_status", "PENDING")
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static void RejectRequest(int StudentID)
        {
            string querry = $"UPDATE enrollement_request SET status = @status, reviewed_at = @reviewed_at WHERE student_id = @student_id AND status = @pending_status";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@status", "REJECTED"),
                new MySqlParameter("@reviewed_at", DateTime.Now),
                new MySqlParameter("@student_id", StudentID),
                new MySqlParameter("@pending_status", "PENDING")
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
    }
}
