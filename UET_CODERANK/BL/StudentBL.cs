using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UET_CODERANK.Model;

namespace UET_CODERANK.BL
{
    class StudentBL
    {
        public static bool RegisterStudent(string reg_No,string name,string email,string password,string leetcode_username) {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            Student student = new Student
            (
                reg_No,
                name,
                email,
                hash,
                leetcode_username
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
    }
}
