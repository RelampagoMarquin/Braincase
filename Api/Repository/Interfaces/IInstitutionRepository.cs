using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IInstitutionRepository: IBaseRepository
    {
        Task<IEnumerable<Institution>> GetAllInstitutions();

        Task<Institution?> GetInstitutionById(Guid id);
    }
}