using Api.Dto.Answer;
using Api.Models;

namespace Api.Dto.Question
{
    public class CreateQuestionDTO
    {
        public String Text { get; set; }

        public byte Type { get; set; }

        public byte Dificult { get; set; }

        public bool IsPrivate { get; set; }

        public String? Justify { get; set; }

        public List<CreateAnswerForQuestionDTO> Answers { get; set; }
        
        public String? InstitutionName { get; set; }

        public List<String> Tags { get; set; }

        public Guid SubjectId { get; set; }
    }

    public class UpdateQuestionDTO
    {
        public String? Text { get; set; } = null;

        public byte? Type { get; set; } = null;

        public byte? Dificult { get; set; } = null;

        public bool? IsPrivate { get; set; } = null;

        public String? Justify { get; set; }

        public Guid? InstitutionId { get; set; } = null;
    }
    public class ResponseQuestionDTO
    {
        public Guid Id { get; set; }

        public String Text { get; set; }

        public byte Type { get; set; }

        public byte Dificult { get; set; }

        public bool IsPrivate { get; set; }

        public String? Justify { get; set; }

        public Guid? InstitutionId { get; set; }

        public String? InstitutionName { get; set; }
    }
}