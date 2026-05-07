using System;

namespace UET_CODERANK.Model
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created_at;
        public Admin(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            Created_at = DateTime.Now;
        }

    }
}
