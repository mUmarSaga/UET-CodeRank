using System;

namespace UET_CODERANK.Model
{
    public class ErrorEntry
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime OccuredAt { get; set; }
        public string Source { get; set; }
    }
}