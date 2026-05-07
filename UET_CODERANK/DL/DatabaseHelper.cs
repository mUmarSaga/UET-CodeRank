using System.Data;
using MySql.Data.MySqlClient;

namespace UET_CODERANK.DL
{
    internal class DatabaseHelper
    {
        

        
        private static readonly string ConnectionString =
            "Server=localhost;Database=ds_final;Uid=root;Pwd=pakistan1947;";

        public static MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        public static int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        public static DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                DataTable dt = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }

        public static object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        public static bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                    return conn.State == ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }
    }
}