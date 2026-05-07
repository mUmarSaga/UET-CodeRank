using System;
namespace UET_CODERANK.Model
{
    public class Batch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Batch(string name, int departmentId)
        {
            this.Name = name;
            this.DepartmentId = departmentId;
            this.CreatedOn = DateTime.Now;
        }
    }
}
