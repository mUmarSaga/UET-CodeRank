using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using UET_CODERANK.Model;
using Windows.UI.Notifications;
using Notification = UET_CODERANK.Model.Notification;
namespace UET_CODERANK.DL
{
    public class NotificationDL
    {
        public static List<Notification> GetByStudentId(int studentId)
        {
            string query = "SELECT * FROM notification WHERE student_id = @student_id ORDER BY created_at DESC";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id", studentId)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(query, parameters);
                var list = new List<Notification>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Notification
                    {
                        Id = Convert.ToInt32(row["id"]),
                        StudentId = Convert.ToInt32(row["student_id"]),
                        Message = row["message"].ToString(),
                        IsRead = Convert.ToBoolean(row["is_read"]),
                        CreatedAt = row["created_at"] != DBNull.Value ? Convert.ToDateTime(row["created_at"]) : DateTime.Now
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "NotificationDL.GetByStudentId");
                throw;
            }
        }

        public static void MarkAsRead(int notificationId)
        {
            string query = "UPDATE notification SET is_read = 1 WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id", notificationId)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "NotificationDL.MarkAsRead");
                throw;
            }
        }

        public static void MarkAllAsRead(int studentId)
        {
            string query = "UPDATE notification SET is_read = 1 WHERE student_id = @student_id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id", studentId)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "NotificationDL.MarkAllAsRead");
                throw;
            }
        }

        public static int GetUnreadCount(int studentId)
        {
            string query = "SELECT COUNT(*) FROM notification WHERE student_id = @student_id AND is_read = 0";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id", studentId)
            };
            try
            {
                return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, parameters));
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "NotificationDL.GetUnreadCount");
                return 0;
            }
        }


        public static void SendNotification(int studentId, string message)
        {
            string query = "INSERT INTO notification(student_id, message, is_read, created_at) VALUES(@student_id, @message, 0, @created_at)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@student_id", studentId),
                new MySqlParameter("@message", message),
                new MySqlParameter("@created_at", DateTime.Now)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "NotificationDL.SendNotification");
            }
        }
    }
}