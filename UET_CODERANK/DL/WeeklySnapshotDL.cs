using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using UET_CODERANK.Model;

namespace UET_CODERANK.DL
{
    public class WeeklySnapshotDL
    {
        public static List<WeeklySnapshot> GetByStudentId(int studentId)
        {
            var list = new List<WeeklySnapshot>();
            try
            {
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@ID", studentId)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "stp_GetSnapshotByStdId",
                    parameters,
                    CommandType.StoredProcedure
                );

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new WeeklySnapshot
                    {
                        Id = Convert.ToInt32(row["id"]),
                        StudentId = Convert.ToInt32(row["student_id"]),
                        WeekStart = Convert.ToDateTime(row["week_start"]),
                        TotalSolved = Convert.ToInt32(row["total_solved"]),
                        EasySolved = Convert.ToInt32(row["easy_solved"]),
                        MediumSolved = Convert.ToInt32(row["medium_solved"]),
                        HardSolved = Convert.ToInt32(row["hard_solved"]),
                        RecordedAt = Convert.ToDateTime(row["recorded_at"])
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "WeeklySnapshotDL.GetByStudentId");
            }
            return list;
        }
    }
}