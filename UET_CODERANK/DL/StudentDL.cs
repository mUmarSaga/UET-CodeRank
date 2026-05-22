using UET_CODERANK.DL;
using MySql.Data.MySqlClient;
using UET_CODERANK.Model;
using System;
namespace UET_CODERANK.DL
{
    public class StudentDL
    {
        public static void AddStudent(Student student)
        {
         
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@std_name",student.Name),
                new MySqlParameter("@REG_NO",student.RegNo),
                new MySqlParameter("@Email",student.Email),
                new MySqlParameter("@Password_",student.Password),
                new MySqlParameter("@Leetcode",student.LeetcodeUsername ?? (object)DBNull.Value),
                new MySqlParameter("@Pp",student.ProfilePicPath ?? (object)DBNull.Value),
                new MySqlParameter("@Pn",student.ProfileName ?? (object)DBNull.Value),
                new MySqlParameter("@Approved",student.IsApproved),
                new MySqlParameter("@created",student.CreatedAt),
                new MySqlParameter("@section",student.SectionId==0 ? (object)DBNull.Value : student.SectionId)
            };
           
            try
            {
                DatabaseHelper.ExecuteNonQuery("stp_AddStudent", parameters,System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static Student GetByLeetCode_username(string leetcodeUsername)
        {
            string querry = "SELECT * FROM student WHERE leetcode_username = @leetcode_username";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@leetcode_username",leetcodeUsername)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                if (dt.Rows.Count == 0) return null;
                var row = dt.Rows[0];
                return new Student(row["reg_no"].ToString(), row["name"].ToString(), row["email"].ToString(), row["password"].ToString(), row["leetcode_username"].ToString(), row["profile_pic_path"].ToString(), row["profile_name"].ToString())
                {
                    Id = Convert.ToInt32(row["id"]),
                    ProfilePicPath = row["profile_pic_path"].ToString(),
                    ProfileName = row["profile_name"].ToString(),
                    IsApproved = Convert.ToBoolean(row["is_approved"]),
                    CreatedAt = Convert.ToDateTime(row["created_at"]),
                    SectionId = Convert.ToInt32(row["section_id"])
                };

            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static Student GetByID(int id)
        {
            string querry = "SELECT * FROM student WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id",id)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                if (dt.Rows.Count == 0) return null;
                var row = dt.Rows[0];
                return new Student(
                    row["reg_no"]?.ToString() ?? "",
                    row["name"]?.ToString() ?? "",
                    row["email"]?.ToString() ?? "",
                    row["password"]?.ToString() ?? "",
                    row["leetcode_username"]?.ToString() ?? "",
                    row["profile_pic_path"]?.ToString() ?? "",
                    row["profile_name"]?.ToString() ?? ""
                )
                {
                    Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0,
                    IsApproved = row["is_approved"] != DBNull.Value ? Convert.ToBoolean(row["is_approved"]) : false,
                    CreatedAt = row["created_at"] != DBNull.Value ? Convert.ToDateTime(row["created_at"]) : DateTime.Now,
                    SectionId = row["section_id"] != DBNull.Value ? Convert.ToInt32(row["section_id"]) : 0
                };
            }
            catch(Exception ex)
             {
                ErrorLog.Log(ex);
                 throw;
             }
            
        }
        public static Student GetByEmail(string email)
        {
            string querry = "SELECT * FROM student WHERE email = @email";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@email",email)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                if (dt.Rows.Count == 0) return null;
                var row = dt.Rows[0];
                return new Student(
                    row["reg_no"]?.ToString() ?? "",
                    row["name"]?.ToString() ?? "",
                    row["email"]?.ToString() ?? "",
                    row["password"]?.ToString() ?? "",
                    row["leetcode_username"]?.ToString() ?? "",
                    row["profile_pic_path"]?.ToString() ?? "",
                    row["profile_name"]?.ToString() ?? ""
                )
                {
                    Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0,
                    IsApproved = row["is_approved"] != DBNull.Value ? Convert.ToBoolean(row["is_approved"]) : false,
                    CreatedAt = row["created_at"] != DBNull.Value ? Convert.ToDateTime(row["created_at"]) : DateTime.Now,
                    SectionId = row["section_id"] != DBNull.Value ? Convert.ToInt32(row["section_id"]) : 0
                };
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static Student GetByRegNo(string regNo)
        {
            string querry = "SELECT * FROM student WHERE reg_no = @reg_no";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@reg_no",regNo)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                if (dt.Rows.Count == 0) return null;
                var row = dt.Rows[0];
                return new Student(
                  row["reg_no"]?.ToString() ?? "",
                  row["name"]?.ToString() ?? "",
                  row["email"]?.ToString() ?? "",
                  row["password"]?.ToString() ?? "",
                  row["leetcode_username"]?.ToString() ?? "",
                  row["profile_pic_path"]?.ToString() ?? "",
                  row["profile_name"]?.ToString() ?? ""
              )
                {
                    Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0,
                    IsApproved = row["is_approved"] != DBNull.Value ? Convert.ToBoolean(row["is_approved"]) : false,
                    CreatedAt = row["created_at"] != DBNull.Value ? Convert.ToDateTime(row["created_at"]) : DateTime.Now,
                    SectionId = row["section_id"] != DBNull.Value ? Convert.ToInt32(row["section_id"]) : 0
                };
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static Student GetById(int id)
        {
            string querry = "SELECT * FROM student WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id",id)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                if (dt.Rows.Count == 0) return null;
                var row = dt.Rows[0];
                return new Student(row["reg_no"].ToString(), row["name"].ToString(), row["email"].ToString(), row["password"].ToString(), row["leetcode_username"].ToString(), row["profile_pic_path"].ToString(), row["profile_name"].ToString())
                {
                    Id = Convert.ToInt32(row["id"]),
                    ProfilePicPath = row["profile_pic_path"].ToString(),
                    ProfileName = row["profile_name"].ToString(),
                    IsApproved = Convert.ToBoolean(row["is_approved"]),
                    CreatedAt = Convert.ToDateTime(row["created_at"]),
                    SectionId = Convert.ToInt32(row["section_id"])
                };
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static void UpdateStudent(Student student)
        {
            string querry = "UPDATE student SET reg_no=@reg_no,email=@email,password=@password,leetcode_username=@leetcode_username,profile_pic_path=@profile_pic_path,is_approved=@is_approved,created_at=@created_at,section_id=@section_id WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@reg_no",student.RegNo),
                new MySqlParameter("@email",student.Email),
                new MySqlParameter("@password",student.Password),
                new MySqlParameter("@leetcode_username",student.LeetcodeUsername),
                new MySqlParameter("@profile_pic_path",student.ProfilePicPath),
                new MySqlParameter("@is_approved",student.IsApproved),
                new MySqlParameter("@created_at",student.CreatedAt),
                new MySqlParameter("@section_id",student.SectionId),
                new MySqlParameter("@id",student.Id)
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
        public static void DeleteStudent(int id)
        {
            string querry = "DELETE FROM student WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id",id)
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
        public static bool IsEmailExists(string email)
        {
            string querry = "SELECT COUNT(*) FROM student WHERE email = @email";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@email",email)
            };
            try
            {
                var count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(querry, parameters));
                return count > 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
        public static bool IsRegNoExists(string regNo)
        {
            string querry = "SELECT COUNT(*) FROM student WHERE reg_no = @reg_no";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@reg_no",regNo)
            };
            try
            {
                var count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(querry, parameters));
                return count > 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                throw;
            }
        }
    }
}
