using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UET_CODERANK.DL;
using UET_CODERANK.Model;

namespace UET_CODERANK.BL
{
    class StudentBL
    {
        public static bool RegisterStudent(string reg_No,string name,string email,string password,string leetcode_username,string profile_pic_path,string profile_name) {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            Student student = new Student
            (
                reg_No,
                name,
                email,
                hash,
                leetcode_username,
                profile_pic_path,
                profile_name
            );
           
            try
            {
                DL.StudentDL.AddStudent(student);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static int LoginStudent(string email, string password)
        {
            Student student = DL.StudentDL.GetByEmail(email);
            if(student == null)
            {
                student = DL.StudentDL.GetByLeetCode_username(email);
            }
            if (student != null && BCrypt.Net.BCrypt.Verify(password, student.Password))
            {
                return student.Id;
            }
            return 0;
        }
        public static bool IsValidEmailFormat(string email) {
            var foo = new EmailAddressAttribute();
            return foo.IsValid(email);
        }
        public static bool IsRegNoFormatValid(string Reg_No) {
            string pattern = @"^20\d{2}-[A-Z]{2,}-\d+$";
            return Regex.IsMatch(Reg_No, pattern);
        }
        public static bool IsRegNoExist(string Reg_No)
        {
            return DL.StudentDL.IsRegNoExists(Reg_No);
        }
        public static bool IsEmailAlreadyExist(string email)
        {
            return DL.StudentDL.IsEmailExists(email);
        }
        public static string UpdateLeetCodeAccount(int studentId, string username, string avatarUrl,string profileName)
        {
            if (string.IsNullOrWhiteSpace(username)) return "Username is required";

            // Check if username taken by another student
            if (StudentDL.IsLeetcodeUsernameTaken(username, studentId))
                return "This LeetCode account is already linked to another student";

            bool success = StudentDL.UpdateLeetCodeAccount(studentId, username, avatarUrl, profileName);
            if (success) return null;
            return "Failed to update account";
        }
    }
}
