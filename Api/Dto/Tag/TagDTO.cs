namespace Api.Dto.Tag
{
    public class CreateTagDTO
    {
        public String Name { get; set; }
        
        public Guid SubjectId { get; set; }
    }

    public class UpdateTagDTO
    {
        public String? Name { get; set; }
        
        public Guid? SubjectId { get; set; } = Guid.Empty;
    }

    public class ResponseTagDTO
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Guid SubjectId { get; set; }

        public String SubjectName { get; set; }
    }

}