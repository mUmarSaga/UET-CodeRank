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
            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            List<Department> list = new List<Department>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Department
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString()
                });
            }
            return list;
        }
    }
}
