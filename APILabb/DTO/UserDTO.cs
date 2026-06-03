
namespace APILabb.DTO
{
    public class UserMakerDTO :UserDTO
    {
    }
    public class UserCreateDTO : UserDTO
    {
        public int ID { get; set; }
        public List<InterestViewDTO> Interests { get; set; }
    }
    public class UserDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class UserWithLinksDTO
    {
        public int ID { get; set; }
        public List<LinkDTO> Links { get; set; }
    }
}
