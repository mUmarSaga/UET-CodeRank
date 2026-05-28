using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UET_CODERANK.Model
{
    public class WeeklySnapshot
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime WeekStart { get; set; }
        public int TotalSolved { get; set; }
        public int EasySolved { get; set; }
        public int MediumSolved { get; set; }
        public int HardSolved { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
