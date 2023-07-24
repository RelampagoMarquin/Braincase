using Api.Dto.Question;
using Api.Dto.Test;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface ITestRepository: IBaseRepository
    {
        Task<IEnumerable<Test>> GetAllTests();

        Task<Test?> GetTestById(Guid id);

        Task<IEnumerable<Test>> GetAllTestsByUser(String id);

        Task<Test> CreateTest(CreateTestDTO createTestDTO, string userid);

        Task<Test> AddQuestionToTest(AddQuestionTetstDTO addQuestionTetstDTO, Test test);

    }
}