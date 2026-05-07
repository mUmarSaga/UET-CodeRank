using Newtonsoft.Json;

namespace UET_CODERANK.Model
{
    public class SolvedStats
    {
        [JsonProperty("solvedProblem")]
        private int totalSolved;

        [JsonProperty("easySolved")]
        private int easySolved;

        [JsonProperty("mediumSolved")]
        private int mediumSolved;

        [JsonProperty("hardSolved")]
        private int hardSolved;

        public int TotalSolved => totalSolved;
        public int EasySolved => easySolved;
        public int MediumSolved => mediumSolved;
        public int HardSolved => hardSolved;
    }
}
