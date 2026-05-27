using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using UET_CODERANK.Model;

namespace UET_CODERANK.DL
{
    public class BadgeDL
    {
        public static List<Badges> GetAllBadges()
        {
            string query = "SELECT * FROM badges";
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(query);
                var list = new List<Badges>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Badges(
                        Convert.ToInt32(row["id"]),
                        row["name"].ToString(),
                        row["description"]?.ToString() ?? "",
                        row["criteria"]?.ToString() ?? "",
                        ""
                    ));
                }
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BadgeDL.GetAllBadges");
                throw;
            }
        }

        public static void AddBadge(Badges badge)
        {
            string query = "INSERT INTO badges(name, description, criteria) VALUES(@name, @description, @criteria)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
            new MySqlParameter("@name", badge.name),
            new MySqlParameter("@description", badge.description),
            new MySqlParameter("@criteria", badge.criteria)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BadgeDL.AddBadge");
                throw;
            }
        }

        public static void DeleteBadge(int badgeId)
        {
            string query = "DELETE FROM badges WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id", badgeId)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BadgeDL.DeleteBadge");
                throw;
            }
        }

        public static void AwardBadge(int studentId, int badgeId)
        {
            string query = "INSERT IGNORE INTO student_badges(student_id, badge_id, awarded_at) VALUES(@student_id, @badge_id, @awarded_at)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id", studentId),
                new MySqlParameter("@badge_id", badgeId),
                new MySqlParameter("@awarded_at", DateTime.Now)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BadgeDL.AwardBadge");
                throw;
            }
        }

        public static List<Badges> GetBadgesByStudentId(int studentId)
        {
            string query = @"SELECT b.* FROM badges b 
                            INNER JOIN student_badges sb ON b.id = sb.badge_id 
                            WHERE sb.student_id = @student_id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id", studentId)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(query, parameters);
                var list = new List<Badges>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Badges(
                        Convert.ToInt32(row["id"]),
                        row["name"].ToString(),
                        row["description"]?.ToString() ?? "",
                        row["criteria"]?.ToString() ?? "",
                        ""
                    ));
                }
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BadgeDL.GetBadgesByStudentId");
                throw;
            }
        }

        public static bool HasBadge(int studentId, int badgeId)
        {
            string query = "SELECT COUNT(*) FROM student_badges WHERE student_id = @student_id AND badge_id = @badge_id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id", studentId),
                new MySqlParameter("@badge_id", badgeId)
            };
            try
            {
                return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, parameters)) > 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BadgeDL.HasBadge");
                throw;
            }
        }
    }
}