
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace UET_CODERANK.DL
{
    internal class LeetCodeStatDL
    {
        public static void AddLeetCodeStat(Model.LeetCodeStat stat)
        {
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@studentId", stat.Student_id),
                new MySqlParameter("@TotalSolved", stat.Total_solved),
                new MySqlParameter("@EasySolved", stat.Easy_solved),
                new MySqlParameter("@MediumSolved", stat.Medium_solved),
                new MySqlParameter("@HardSolved", stat.Hard_solved),
                new MySqlParameter("@Ranking", stat.Global_rank),
                new MySqlParameter("@LastUpdated", DateTime.Now)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery("stp_InsertLeetcodeStats", parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static void UpdateLeetCodeStat(Model.LeetCodeStat stat)
        {
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@StdId",stat.Student_id),
                new MySqlParameter("@Total",stat.Total_solved),
                new MySqlParameter("@Easy",stat.Easy_solved),
                new MySqlParameter("@MediumSolved",stat.Medium_solved),
                new MySqlParameter("@Hard",stat.Hard_solved),
                new MySqlParameter("@ranking",stat.Global_rank),
                new MySqlParameter("@lastUpdated",DateTime.Now)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery("stp_UpdateLeetcodeStatsByStdId", parameters,CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static void DeleteLeetCodeStat(int student_id)
        {
            string querry = "DELETE FROM leetcode_stat WHERE student_id = @student_id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id",student_id)
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
        public static Model.LeetCodeStat GetLeetCodeStatByStudentId(int student_id)
        {
            string querry = "SELECT * FROM leetcode_stats WHERE student_id = @student_id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id",student_id)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                if (dt.Rows.Count == 0) return null;
                var row = dt.Rows[0];
                return new Model.LeetCodeStat(student_id, Convert.ToInt32(row["total_solved"]), Convert.ToInt32(row["easy_solved"]), Convert.ToInt32(row["medium_solved"]), Convert.ToInt32(row["hard_solved"]), Convert.ToInt32(row["global_ranking"]))
                {
               
                    Last_updated = Convert.ToDateTime(row["last_updated"])
                };
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
    }
}
