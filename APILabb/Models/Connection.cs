namespace APILabb.Models
{
    public class Connection
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

        public int InterestID { get; set; }
        public Interest Interest { get; set; }

        public string? URL { get; set; }
    }
}
