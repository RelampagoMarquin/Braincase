using Api.Dto.Answer;
using Api.Dto.Favorites;
using Api.Dto.Tag;
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
    public class ResponseQuestionDTO
    {
        public Guid Id { get; set; }

        public String Text { get; set; }

        public byte Type { get; set; }

        public byte Dificult { get; set; }

        public bool IsPrivate { get; set; }

        public String? Justify { get; set; }

        public Guid? InstitutionId { get; set; }

        public List<ResponseFavoritesDTO> Favorites { get; set; }

        public String Criador { get; set; }

        public String Email { get; set; }

        public List<ResponseAnswerDTO> Answers { get; set; }

        public List<ResponseTagDTO> Tags { get; set; }

        public String? InstitutionName { get; set; }
    }
}