
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace UET_CODERANK.DL
{
    internal class LeetCodeStatDL
    {
        public static void UpsertLeetCodeStat(Model.LeetCodeStat stat)
        {
            MySqlParameter[] parameters = new MySqlParameter[]
            {
        new MySqlParameter("@p_student_id",     stat.Student_id),
        new MySqlParameter("@p_total_solved",   stat.Total_solved),
        new MySqlParameter("@p_easy_solved",    stat.Easy_solved),
        new MySqlParameter("@p_medium_solved",  stat.Medium_solved),
        new MySqlParameter("@p_hard_solved",    stat.Hard_solved),
        new MySqlParameter("@p_global_ranking", stat.Global_rank),
        new MySqlParameter("@p_last_updated",   DateTime.Now)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery("stp_UpsertLeetcodeStats", parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "LeetCodeStatDL.UpsertLeetCodeStat");
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
                ErrorLog.Log(ex, "LeetCodeStatDL.DeleteLeetCodeStat");
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
                ErrorLog.Log(ex, "LeetCodeStatDL.GetLeetCodeStatByStudentId");
                throw;
            }
        }
    }
}
