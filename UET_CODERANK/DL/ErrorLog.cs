using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace UET_CODERANK.DL
{
    public static class ErrorLog
    {
        public static void Log(Exception ex, string source = "")
        {
            try
            {
                string query = @"INSERT INTO error_log 
                                (error_message, stack_trace, occured_at, source) 
                                VALUES (@msg, @stack, @time, @source)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@msg",    ex.Message),
                    new MySqlParameter("@stack",  ex.StackTrace ?? ""),
                    new MySqlParameter("@time",   DateTime.Now),
                    new MySqlParameter("@source", source)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch { } 
        }

        public static List<Model.ErrorEntry> GetAll()
        {
            try
            {
                string query = "SELECT * FROM error_log ORDER BY occured_at DESC";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                var list = new List<Model.ErrorEntry>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Model.ErrorEntry
                    {
                        Id = Convert.ToInt32(row["id"]),
                        ErrorMessage = row["error_message"].ToString(),
                        StackTrace = row["stack_trace"].ToString(),
                        OccuredAt = Convert.ToDateTime(row["occured_at"]),
                        Source = row["source"].ToString()
                    });
                }
                return list;
            }
            catch { return new List<Model.ErrorEntry>(); }
        }
        public static void ClearAll()
        {
            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM error_log");
            }
            catch { }
        }
    }
}