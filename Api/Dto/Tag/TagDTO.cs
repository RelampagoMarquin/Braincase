namespace Api.Dto.Tag
{
    public class TagDTO
    {
        public String Name { get; set; }

        public Api.Models.Subject? Subject {get; set; }
    }
}