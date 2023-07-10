namespace Api.Dto.Answer
{
    public class CreateAnswerDTO
    {
        public String Text { get; set; }

        public Boolean IsCorrect { get; set; }

        public Guid QuestionId { get; set; } 
    }

    public class CreateAnswerForQuestionDTO
    {
        public String Text { get; set; }

        public Boolean IsCorrect { get; set; }
    }

    public class UpdateAnswerDTO
    {
        public String? Text { get; set; }

        public Boolean? IsCorrect { get; set;}
        
    }

    public class ResponseAnswerDTO
    {
        public Guid Id { get; set; }

        public String Text { get; set; }

        public Boolean IsCorrect { get; set;}
        
        public Guid QuestionId { get; set; }
    }
}