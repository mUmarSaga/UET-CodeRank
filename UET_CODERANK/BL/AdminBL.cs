using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UET_CODERANK.DL;
using UET_CODERANK.Model;

namespace UET_CODERANK.BL
{
    class AdminBL
    {
        public static bool AddAdmin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < 6)
                return false;

            if (AdminDL.GetByUsername(username) != null)
                return false;

            string hashed = BCrypt.Net.BCrypt.HashPassword(password);
            Admin admin = new Admin(username, hashed);
            try
            {
                AdminDL.AddAdmin(admin);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteAdmin(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            if (CurrentSession.Admin?.Username == username)
                return false;

            Admin admin = AdminDL.GetByUsername(username);
            if (admin == null)
                return false;

            try
            {
                AdminDL.DeleteAdmin(admin);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static int LoginAdmin(string username, string password)
        {
            Admin admin = AdminDL.GetByUsername(username);
            if (admin != null && BCrypt.Net.BCrypt.Verify(password, admin.Password))
                return admin.Id;
            return 0;
        }
    }
}