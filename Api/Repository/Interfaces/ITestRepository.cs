using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface ITestRepository: IBaseRepository
    {
        Task<IEnumerable<Test>> GetAllTests();

        Task<Test?> GetTestById(Guid id);

        Task<IEnumerable<Test>> GetAllTestsByUser(Guid id);
    }
}