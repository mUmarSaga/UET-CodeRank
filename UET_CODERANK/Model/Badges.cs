namespace UET_CODERANK.Model
{
    public class Badges
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string criteria { get; set; }
        public string image_url { get; set; }
        public Badges(int id,string name,string description,string criteria,string image_url)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.criteria = criteria;
            this.image_url = image_url;
        }
    }
}
