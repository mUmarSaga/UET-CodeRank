using Newtonsoft.Json;

using System;
namespace UET_CODERANK.Model
{
    public class ContestStats
    {
        private int studentId;
        [JsonProperty("contestAttend")]
        private int contestAttended;

        [JsonProperty("contestRating")]
        private float contestRating;

        [JsonProperty("contestGlobalRanking")]
        private int globalContestRank;

        [JsonProperty("contestTopPercentage")]
        private float topPercentage;
        private DateTime Last_updated;
        public int ContestAttended => contestAttended;
        public float ContestRating => contestRating;
        public int GlobalContestRank => globalContestRank;
        public float TopPercentage => topPercentage;
        public ContestStats(int studentId, int contestAttended, float contestRating, int globalContestRank, float topPercentage)
        {
            this.studentId = studentId;
            this.contestAttended = contestAttended;
            this.contestRating = contestRating;
            this.globalContestRank = globalContestRank;
            this.topPercentage = topPercentage;
            this.Last_updated = DateTime.Now;
        }
    }
}
