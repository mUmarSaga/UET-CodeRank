using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UET_CODERANK.DL;
using UET_CODERANK.Model;

namespace UET_CODERANK.BL
{
    class UpdateProfileBL
    {
        public static bool UpdateProfile(int id, string name, string email, string leetcodeUsername, string profileName, string profilePicPath)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
                return false;

            var emailAttr = new EmailAddressAttribute();
            if (!emailAttr.IsValid(email))
                return false;

            Student existing = StudentDL.GetByEmail(email);
            if (existing != null && existing.Id != id)
                return false;

            try
            {
                UpdateProfileDL.UpdateProfile(id, name, email, leetcodeUsername, profileName, profilePicPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UpdatePassword(int id, string currentPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                return false;

            Student student = StudentDL.GetById(id);
            if (student == null)
                return false;

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, student.Password))
                return false;

            string hashed = BCrypt.Net.BCrypt.HashPassword(newPassword);
            try
            {
                UpdateProfileDL.UpdatePassword(id, hashed);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}