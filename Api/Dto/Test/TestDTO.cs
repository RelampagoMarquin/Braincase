namespace Api.Dto.Test
{
    public class CreateTestDTO
    {
        public String Name { get; set; }

        public String ClassName { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime LastUse { get; set; }

        public String? LogoUrl { get; set; }
    }

    public class UpdateTestDTO
    {
        public String? Name { get; set; }

        public String? ClassName { get; set; }

        public DateTime? LastUse { get; set; }

        public String? LogoUrl { get; set; }
    }

    public class ResponseTestDTO
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String ClassName { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime LastUse { get; set; }

        public String? LogoUrl { get; set; }

        public Guid User { get; set; }
    }

}