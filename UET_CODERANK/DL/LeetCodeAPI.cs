using System.Net.Http;
using UET_CODERANK.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UET_CODERANK.DL
{
    public class Data
    {
        // This is what becomes null if the user doesn't exist
        public object matchedUser { get; set; }
    }
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
                if(leetCodeProfile.Username == null)
                {
                    return null;
                }
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
                LeetCodeStat leetCodeStat2 = Newtonsoft.Json.JsonConvert.DeserializeObject<LeetCodeStat>(json2);
                leetCodeStat.Global_rank = leetCodeStat2.Global_rank;
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
