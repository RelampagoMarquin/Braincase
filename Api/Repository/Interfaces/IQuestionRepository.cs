using Api.Dto.Question;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IQuestionRepository : IBaseRepository
    {
        Task<IEnumerable<Question>> GetAllQuestions();

        Task<Question?> GetQuestionById(Guid id);

        Task<IEnumerable<Question>> GetAllPublic();

        Task<IEnumerable<Question>> GetByUserQuestion(string id);

        // este get pega todas as questões publicas e soma com as privadas do usuário
        Task<IEnumerable<Question>> GetAllUserQuestion(string id);
    }
}