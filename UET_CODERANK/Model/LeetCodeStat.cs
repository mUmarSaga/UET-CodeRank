using Newtonsoft.Json;
using System;
namespace UET_CODERANK.Model
{
    public class LeetCodeStat
    {
        public int Id { get; set; }
        
        public int Student_id { get; set; }
        [JsonProperty("solvedProblem")]
        public int Total_solved { get; set; }
        [JsonProperty("easySolved")]
        public int Easy_solved { get; set; }
        [JsonProperty("mediumSolved")]
        public int Medium_solved { get; set; }
        [JsonProperty("hardSolved")]
        public int Hard_solved { get; set; }
        [JsonProperty("ranking")]
        public int Global_rank { get; set; }
        public DateTime Last_updated { get; set; }
        public LeetCodeStat(int student_id, int total_solved, int easy_solved, int medium_solved, int hard_solved, int global_rank)
        {
            this.Student_id = student_id;
            this.Total_solved    = total_solved;
            this.Easy_solved = easy_solved;
            this.Medium_solved = medium_solved;
            this.Hard_solved = hard_solved;
            this.Global_rank = global_rank;
            this.Last_updated = DateTime.Now;
        }
    }
}
