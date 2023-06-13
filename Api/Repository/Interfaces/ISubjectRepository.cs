using Api.Dto.Subject;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface ISubjectRepository : IBaseRepository
    {
        Task<IEnumerable<Subject>> GetAllSubject();

        Task<Subject?> GetSubjectById(Guid id); 

    }
}