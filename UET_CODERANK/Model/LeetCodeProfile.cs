
using Newtonsoft.Json;

namespace UET_CODERANK.Model
{
    public class LeetCodeProfile
    {
        [JsonProperty("username")]
        private string username;

        [JsonProperty("name")]
        private string name;

        [JsonProperty("ranking")]
        private int ranking;

        [JsonProperty("avatar")]
        private string avatar;

        [JsonProperty("country")]
        private string country;

        public string Username => username;
        public string Name => name;
        public int Ranking => ranking;
        public string Avatar => avatar;

        public string Country => country;
    }
}
