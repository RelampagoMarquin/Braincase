namespace Api.Dto.Question
{
    public class CreateQuestionDTO
    {
        public String Text { get; set; }

        public byte Type { get; set; }

        public byte Dificult { get; set; }

        public bool IsPrivate { get; set; }

        public Guid? InstitutionId { get; set; }
    }

    public class UpdateQuestionDTO
    {
        public String? Text { get; set; } = null;

        public byte? Type { get; set; } = null;

        public byte? Dificult { get; set; } = null;

        public bool? IsPrivate { get; set; } = null;

        public Guid? InstitutionId { get; set; } = null;
    }
    public class ResponseQuestionDTO
    {
        public Guid Id { get; set; }

        public String Text { get; set; }

        public byte Type { get; set; }

        public byte Dificult { get; set; }

        public bool IsPrivate { get; set; }

        public Guid? InstitutionId { get; set; }
    }
}