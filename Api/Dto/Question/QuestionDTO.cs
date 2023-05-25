namespace Api.Dto.Question
{
    public class QuestionCreateDTO
    {
        public String Text { get; set; }

        public byte Type { get; set; }  = 1;

        public byte Dificult { get; set; } = 2;

        public bool IsPrivate { get; set; } = true;
    }
    public class QuestionResponseDTO
    {
        public String Text { get; set; }

        public byte Type { get; set; }

        public byte Dificult { get; set; }

        public bool IsPrivate { get; set; }
    }
}