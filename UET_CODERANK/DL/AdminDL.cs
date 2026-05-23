
using MySql.Data.MySqlClient;
using System;
namespace UET_CODERANK.DL
{
    internal class AdminDL
    {
        public static void AddAdmin(Model.Admin admin)
        {
            string querry = "INSERT INTO admin(username,password,created_at) Values(@username,@password,@created_at)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@username",admin.Username),
                new MySqlParameter("@password",admin.Password),
                new MySqlParameter("@created_at",admin.Created_at)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static Model.Admin GetByUsername(string username)
        {
            string querry = "SELECT * FROM admin WHERE username = @name";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@name",username)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                if (dt.Rows.Count == 0) return null;
                var row = dt.Rows[0];
                return new Model.Admin(row["username"].ToString(), row["password"].ToString())
                {
                    Id = Convert.ToInt32(row["id"]),
                    Created_at = Convert.ToDateTime(row["created_at"])
                };
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
  
        public static void DeleteAdmin(Model.Admin admin)
        {
            string querry = "DELETE FROM admin WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id",admin.Id)
            };
            try
            {
            DatabaseHelper.ExecuteNonQuery(querry, parameters);
                
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static void UpdateAdmin(Model.Admin admin)
        {
            string querry = "UPDATE admin SET username = @username, password = @password WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@username",admin.Username),
                new MySqlParameter("@password",admin.Password),
                new MySqlParameter("@id",admin.Id)
            };
            try
            {
                 DatabaseHelper.ExecuteNonQuery(querry, parameters);
                
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }

        }

        public static System.Collections.Generic.List<Model.Admin> GetAllAdmins()
        {
            string querry = "SELECT * FROM admin";
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, new MySqlParameter[0]);
                var list = new System.Collections.Generic.List<Model.Admin>();
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    var admin = new Model.Admin(row["username"].ToString(), row["password"].ToString())
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Created_at = Convert.ToDateTime(row["created_at"])
                    };
                    list.Add(admin);
                }
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
    }
}
