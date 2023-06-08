namespace Api.Dto.Question
{
    public class CreateQuestionDTO
    {
        public String Text { get; set; }

        public byte Type { get; set; }

        public byte Dificult { get; set; }

        public bool IsPrivate { get; set; }

        public Guid? Institution { get; set; }
    }

    public class UpdateQuestionDTO
    {
        public String? Text { get; set; }

        public byte? Type { get; set; }

        public byte? Dificult { get; set; }

        public bool? IsPrivate { get; set; }

        public Guid? Institution { get; set; }
    }
    public class ResponseQuestionDTO
    {
        public Guid Id { get; set; }

        public String Text { get; set; }

        public byte Type { get; set; }

        public byte Dificult { get; set; }

        public bool IsPrivate { get; set; }

        public Guid? Institution { get; set; }
    }
}