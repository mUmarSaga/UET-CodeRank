namespace UET_CODERANK.Model
{
    public class LeaderboardEntry
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int TotalSolved { get; set; }
        public int EasySolved { get; set; }
        public int MediumSolved { get; set; }
        public int HardSolved { get; set; }
        public int GlobalRanking { get; set; }
        public float ContestRating { get; set; }
        public int ContestAttended { get; set; }
    }
}