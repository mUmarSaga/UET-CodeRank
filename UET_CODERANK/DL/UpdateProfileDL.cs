using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UET_CODERANK.DL
{
    public class UpdateProfileDL
    {
        public static void UpdateProfile(int id, string name, string email)
        {
            string query = "UPDATE student SET name=@name, email=@email WHERE id=@id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@name", name),
                new MySqlParameter("@email", email),
                new MySqlParameter("@id", id)
            };
            
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "UpdateProfileDL.UpdateProfile");
                throw;
            }
        }

        public static void UpdatePassword(int id, string hashedPassword)
        {
            string query = "UPDATE student SET password=@password WHERE id=@id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@password", hashedPassword),
                new MySqlParameter("@id", id)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "UpdateProfileDL.UpdatePassword");
                throw;
            }
        }
    }
}