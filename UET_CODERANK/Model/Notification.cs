using System;

namespace UET_CODERANK.Model
{
    public class Notification
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}