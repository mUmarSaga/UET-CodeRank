using System;
using System.Collections.Generic;
using System.Text;

namespace UET_CODERANK.DL
{
    internal class ContestStatsDL
    {
        public static void AddContestStats(Model.ContestStats stats)
        {
            string querry = "INSERT INTO contest_stats(student_id,contest_rating,contest_attended,global_contest_rank,last_updated) VALUES(@student_id,@contest_rating,@contest_attended,@global_contest_rank,@last_updated)";
            MySql.Data.MySqlClient.MySqlParameter[] parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                    //new MySql.Data.MySqlClient.MySqlParameter("@student_id",stats.StudentId),
                    //new MySql.Data.MySqlClient.MySqlParameter("@contest_rating",stats.ContestRating),
                    //new MySql.Data.MySqlClient.MySqlParameter("@contest_attended",stats.ContestAttended),
                    //new MySql.Data.MySqlClient.MySqlParameter("@global_contest_rank",stats.GlobalContestRank),
                    //new MySql.Data.MySqlClient.MySqlParameter("@last_updated",stats.LastUpdated)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "ContestStatsDL.AddContestStats");
                throw;
            }
        }
    }
}
