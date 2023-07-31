using Api.Dto.Question;
using Api.Models;

namespace Api.Dto.Test
{
    public class CreateTestDTO
    {
        public String Name { get; set; }

        public String ClassName { get; set; }

        public String? LogoUrl { get; set; }

    }

    public class AddQuestionTetstDTO
    {
        public List<Models.Question> Questions { get; set; }
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

        public String UserId { get; set; }

        public Byte NQuestion { get; set; }
    }

    public class ResponseQuestionTestDTO: ResponseTestDTO
    {
        public List<ResponseQuestionDTO> Questions { get; set; }

    }
}