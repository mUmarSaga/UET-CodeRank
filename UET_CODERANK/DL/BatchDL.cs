using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace UET_CODERANK.DL
{
    internal class BatchDL
    {
        public static void AddBatch(Model.Batch batch)
        {
            string querry = "INSERT INTO batch(name,department_id,created_on) Values(@name,@department_id,@created_on)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                    new MySqlParameter("@name",batch.Name),
                    new MySqlParameter("@department_id",batch.DepartmentId),
                    new MySqlParameter("@created_on",batch.CreatedOn)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BatchDL.AddBatch");
                throw;
            }
        }
        public static void DeleteBatch(Model.Batch batch)
        {
            string querry = "DELETE FROM batch WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id",batch.Id)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);

            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BatchDL.DeleteBatch");
                throw;
            }
        }
        public static void UpdateBatch(Model.Batch batch)
        {
            string querry = "UPDATE batch SET name = @name, department_id = @department_id WHERE id = @id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@name",batch.Name),
                new MySqlParameter("@department_id",batch.DepartmentId),
                new MySqlParameter("@id",batch.Id)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery(querry, parameters);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BatchDL.UpdateBatch");
                throw;
            }
        }
        public static Model.Batch GetBatchById(int id)
        {
            string querry = "SELECT * FROM batch WHERE id = @id";
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
                    return new Model.Batch(row["name"].ToString(), Convert.ToInt32(row["department_id"]))
                    {
                        Id = Convert.ToInt32(row["id"]),
                        CreatedOn = Convert.ToDateTime(row["created_on"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BatchDL.GetBatchById");
                throw;
            }
        }
        public static List<Model.Batch> GetBatchesByDepartmentId(int departmentId)
        {
            string querry = "SELECT * FROM batch WHERE department_id = @department_id";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@department_id",departmentId)
            };
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry, parameters);
                List<Model.Batch> batches = new List<Model.Batch>();
                foreach (DataRow row in dt.Rows)
                {
                    batches.Add(new Model.Batch(row["name"].ToString(), Convert.ToInt32(row["department_id"]))
                    {
                        Id = Convert.ToInt32(row["id"]),
                        CreatedOn = Convert.ToDateTime(row["created_on"])
                    });
                }
                return batches;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BatchDL.GetBatchesByDepartmentId");
                throw;
            }
        }
        public static List<Model.Batch> GetAllBatches()
        {
            string querry = "SELECT * FROM batch";
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(querry);
                List<Model.Batch> batches = new List<Model.Batch>();
                foreach (DataRow row in dt.Rows)
                {
                    batches.Add(new Model.Batch(row["name"].ToString(), Convert.ToInt32(row["department_id"]))
                    {
                        Id = Convert.ToInt32(row["id"]),
                        CreatedOn = Convert.ToDateTime(row["created_on"])
                    });
                }
                return batches;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex, "BatchDL.GetAllBatches");
                throw;
            }
        }
    }
}
