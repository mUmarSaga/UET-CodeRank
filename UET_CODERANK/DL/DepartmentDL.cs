using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UET_CODERANK.Model;
namespace UET_CODERANK.DL
{
    internal class DepartmentDL
    {
        public static List<Department> GetAll()
        {
            string query = "SELECT * FROM department";
            DataTable dt;
            try
            {
                 dt = DatabaseHelper.ExecuteQuery(query);
            }
            catch(Exception ex) {
            {
                ErrorLog.Log(ex, "DepartmentDL.GetAll");
                    return null;
            }
            List<Department> list = new List<Department>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Department
                {
                    Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0,
                    Name = row["name"] != DBNull.Value ? row["name"].ToString() : ""
                });
            }
            return list;
        }
    }
}
