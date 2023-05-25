namespace Api.Dto.Answer
{
    public class CreateAnswerDTO
    {
        public String Text { get; set; }

        public Boolean IsCorrect { get; set;}
        
        public String Justify { get; set; }

    }

    public class ResponseAnswerDTO
    {
        public Guid Id { get; set; }
        public String Text { get; set; }

        public Boolean IsCorrect { get; set;}
        
        public String Justify { get; set; }
    }
}