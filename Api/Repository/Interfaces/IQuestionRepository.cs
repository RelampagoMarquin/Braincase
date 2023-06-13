using Api.Dto.Question;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IQuestionRepository : IBaseRepository
    {
        Task<IEnumerable<Question>> GetAllQuestions();

        Task<Question?> GetQuestionById(Guid id);
    }
}