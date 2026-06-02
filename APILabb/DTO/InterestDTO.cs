namespace APILabb.DTO
{
    public class InterestDTO
    {
        public int ID { get; set; }
        public string InterestName { get; set; }
        public string? Description { get; set; }
    }
    public class InterestViewDTO
    {
        public int ID { get; set; }
        public string InterestName { get; set; }
        public string? Description { get; set; }

        public List<LinkDTO> Links { get; set; }
    }
    public class InterestCreateDTO
    {
        public string InterestName { get; set; }
        public string? Description { get; set; }
    }
}
