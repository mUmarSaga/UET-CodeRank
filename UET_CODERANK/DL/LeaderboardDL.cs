using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using UET_CODERANK.Model;

namespace UET_CODERANK.DL
{
    public class LeaderboardDL
    {
        public static List<LeaderboardEntry> GetLeaderboard()
        {
            DataTable dt = DatabaseHelper.ExecuteQuery("stp_GetLeaderboard", null, CommandType.StoredProcedure);
            if(dt.Rows.Count == 0) return new List<LeaderboardEntry>();
            return MapToEntries(dt);
        }

        public static List<LeaderboardEntry> GetLeaderboardByBatch(int batchId)
        {
            MySqlParameter[] p = { new MySqlParameter("@BatchID", batchId) };
            DataTable dt = DatabaseHelper.ExecuteQuery("stp_GetLeaderboardByBatch", p, CommandType.StoredProcedure);
            if(dt.Rows.Count == 0) return new List<LeaderboardEntry>();
            return MapToEntries(dt);
        }
        public static List<LeaderboardEntry> GetLeaderboardBySection(int sectionId)
        {
            MySqlParameter[] p = { new MySqlParameter("@SectionID", sectionId) };
            DataTable dt = DatabaseHelper.ExecuteQuery("stp_GetLeaderboardBySection", p, CommandType.StoredProcedure);
            if (dt.Rows.Count == 0) return new List<LeaderboardEntry>();
            return MapToEntries(dt);
        }
        private static List<LeaderboardEntry> MapToEntries(DataTable dt)
        {
            List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
            foreach (DataRow row in dt.Rows)
            {
                entries.Add(new LeaderboardEntry
                {
                    StudentId = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    RegNo = row["reg_no"].ToString(),
                    SectionId = Convert.ToInt32(row["section_id"]),
                    SectionName = row["section_name"].ToString(),
                    BatchId = Convert.ToInt32(row["batch_id"]),
                    BatchName = row["batch_name"].ToString(),
                    TotalSolved = row["total_solved"] == DBNull.Value ? 0 : Convert.ToInt32(row["total_solved"]),
                    EasySolved = row["easy_solved"] == DBNull.Value ? 0 : Convert.ToInt32(row["easy_solved"]),
                    MediumSolved = row["medium_solved"] == DBNull.Value ? 0 : Convert.ToInt32(row["medium_solved"]),
                    HardSolved = row["hard_solved"] == DBNull.Value ? 0 : Convert.ToInt32(row["hard_solved"]),
                    GlobalRanking = row["global_ranking"] == DBNull.Value ? 0 : Convert.ToInt32(row["global_ranking"]),
                    ContestRating = row["contest_rating"] == DBNull.Value ? 0 : Convert.ToSingle(row["contest_rating"]),
                    ContestAttended = row["contest_attended"] == DBNull.Value ? 0 : Convert.ToInt32(row["contest_attended"])
                });
            }
            return entries;
        }
    }
}