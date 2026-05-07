namespace UET_CODERANK.Model
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Batch_id { get; set; }

        public Section(string name, int batch_id)
        {
            this.Name = name;
            this.Batch_id = batch_id;
        }
    }
}
