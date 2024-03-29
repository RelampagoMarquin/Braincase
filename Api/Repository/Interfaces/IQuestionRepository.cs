using Api.Dto.Question;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IQuestionRepository : IBaseRepository
    {
        Task<IEnumerable<Question>> GetAllQuestions();

        Task<Question?> GetQuestionById(Guid id);

        Task<IEnumerable<Question>> GetAllPublic();

        Task<IEnumerable<Question>> GetMyQuestions(string id);

        Task<IEnumerable<Question>> GetMyFavorites(string id);

        // este get pega todas as questões publicas e soma com as privadas do usuário
        Task<IEnumerable<Question>> GetAllUserQuestion(string id);

        Task<Question> CreateQuestion(CreateQuestionDTO createQuestionDTO, string userid);

        Task<Question> UpdateQuestion(UpdateQuestionDTO updateQuestionDTO, Question question);
    }
}