using System;

namespace UET_CODERANK.Model
{
    public class EnrollementRequest
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SectionId { get; set; }
        public EnrollementStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }

    }
}
