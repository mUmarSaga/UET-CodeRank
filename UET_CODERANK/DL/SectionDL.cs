using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

namespace UET_CODERANK.DL
{
    internal class SectionDL
    {
        public static void AddSection(Model.Section section)
        {
            string querry = "INSERT INTO section(name,batch_id) Values(@name,@batch_id)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                    new MySqlParameter("@name",section.Name),
                    new MySqlParameter("@batch_id",section.Batch_id)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex,"SectionDL.AddSection");
                throw;
            }
        }
        public static void UpdateSection(Model.Section section)
        {
            string querry = "UPDATE section SET name=@name,batch_id=@batch_id WHERE id=@id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                    new MySqlParameter("@name",section.Name),
                    new MySqlParameter("@batch_id",section.Batch_id),
                    new MySqlParameter("@id",section.Id)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "SectionDL.UpdateSection");
                throw;
            }
        }
        public static void DeleteSection(Model.Section section)
        {
            string querry = "DELETE FROM section WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                    new MySqlParameter("@id",section.Id)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "SectionDL.DeleteSection");
                throw;
            }
        }
        public static List<Model.Section> GetSectionsByBatchId(int batchId)
        {
            string querry = "SELECT * FROM section WHERE batch_id = @batch_id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                    new MySqlParameter("@batch_id",batchId)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                List<Model.Section> sections = new List<Model.Section>();
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    sections.Add(new Model.Section(row["name"].ToString(), Convert.ToInt32(row["batch_id"]))
                    {
                        Id = Convert.ToInt32(row["id"])
                    });
                }
                return sections;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "SectionDL.GetSectionsByBatchId");
                throw;
            }
        }
        public static List<Model.Section> GetAllSections()
        {
            string querry = "SELECT * FROM section";
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry);
                List<Model.Section> sections = new List<Model.Section>();
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    sections.Add(new Model.Section(row["name"].ToString(), Convert.ToInt32(row["batch_id"]))
                    {
                        Id = Convert.ToInt32(row["id"])
                    });
                }
                return sections;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "SectionDL.GetAllSections");
                throw;
            }
        }
        public static Model.Section GetSectionById(int id)
        {
            string querry = "SELECT * FROM section WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                        new MySqlParameter("@id",id)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    return new Model.Section(row["name"].ToString(), Convert.ToInt32(row["batch_id"]))
                    {
                        Id = Convert.ToInt32(row["id"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "SectionDL.GetSectionById");
                throw;
            }
        }

    }
}
