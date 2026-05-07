using System;

namespace UET_CODERANK.Model
{
    public class Student
    {

        public int Id { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LeetcodeUsername { get; set; }
        public string ProfilePicPath { get; set; }
        public bool IsApproved { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public int SectionId { get; set; }
        public Student(string RegNo,string Name,string Email,string Password,string LeetcodeUsername)
        {
            this.RegNo = RegNo;
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            CreatedAt = DateTime.Now;
            IsApproved  = false;
        }
    }
}
