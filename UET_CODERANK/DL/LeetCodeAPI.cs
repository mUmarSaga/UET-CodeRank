using System.Net.Http;
using UET_CODERANK.Model;

namespace CodeRank.DL
{
    internal class LeetCodeAPI
    {
        private static HttpClient client = new HttpClient();
        public static LeetCodeProfile GetProfile(string username)
        {
            try
            {
                string url = $"https://alfa-leetcode-api.onrender.com/{username}";
                string json = client.GetStringAsync(url).Result;
                LeetCodeProfile leetCodeProfile = Newtonsoft.Json.JsonConvert.DeserializeObject<LeetCodeProfile>(json);
                return leetCodeProfile;
            }
            catch { 
                return null;
            }
        }
        public static LeetCodeStat GetSolvedStats(string username)
        {
            try
            {
                string url = $"https://alfa-leetcode-api.onrender.com/{username}/solved";
                string json = client.GetStringAsync(url).Result;
                LeetCodeStat leetCodeStat = Newtonsoft.Json.JsonConvert.DeserializeObject<LeetCodeStat>(json);
                string url2 = $"https://alfa-leetcode-api.onrender.com/{username}";
                string json2 = client.GetStringAsync(url2).Result;
                leetCodeStat = Newtonsoft.Json.JsonConvert.DeserializeObject<LeetCodeStat>(json2);
                return leetCodeStat;
            } catch { return null; }
        }
        public static ContestStats GetContestStats(string username)
        {
            try
            {
                string url = $"https://alfa-leetcode-api.onrender.com/{username}/contest";
                string json = client.GetStringAsync(url).Result;
                ContestStats contestStats = Newtonsoft.Json.JsonConvert.DeserializeObject<ContestStats>(json);
                return contestStats;
            }
            catch { return null; }
        }

    }
}
