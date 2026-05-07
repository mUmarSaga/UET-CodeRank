
using MySql.Data.MySqlClient;
using System;

namespace UET_CODERANK.DL
{
    internal class LeetCodeStatDL
    {
        public static void AddLeetCodeStat(Model.LeetCodeStat stat)
        {
            string querry = "INSERT INTO leetcode_stat(student_id,total_solved,easy_solved,medium_solved,hard_solved,global_ranking,last_updated) Values(@student_id,@total_solved,@easy_solved,@medium_solved,@hard_solved,@global_ranking,@last_updated)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id",stat.Student_id),
                new MySqlParameter("@total_solved",stat.Total_solved),
                new MySqlParameter("@easy_solved",stat.Easy_solved),
                new MySqlParameter("@medium_solved",stat.Medium_solved),
                new MySqlParameter("@hard_solved",stat.Hard_solved),
                new MySqlParameter("@global_ranking",stat.Global_rank),
                new MySqlParameter("@last_updated",DateTime.Now)
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
        public static void UpdateLeetCodeStat(Model.LeetCodeStat stat)
        {
            string querry = "UPDATE leetcode_stat SET total_solved=@total_solved,easy_solved=@easy_solved,medium_solved=@medium_solved,hard_solved=@hard_solved,global_ranking=@global_ranking,last_updated=@last_updated WHERE student_id=@student_id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id",stat.Student_id),
                new MySqlParameter("@total_solved",stat.Total_solved),
                new MySqlParameter("@easy_solved",stat.Easy_solved),
                new MySqlParameter("@medium_solved",stat.Medium_solved),
                new MySqlParameter("@hard_solved",stat.Hard_solved),
                new MySqlParameter("@global_ranking",stat.Global_rank),
                new MySqlParameter("@last_updated",DateTime.Now)
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
            string querry = "SELECT * FROM leetcode_stat WHERE student_id = @student_id";
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
