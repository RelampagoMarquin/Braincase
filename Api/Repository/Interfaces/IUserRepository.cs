using Api.Dto.User;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IUserRepository: IBaseRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(String id);

        Task<User> CreateUser(UserCreateDTO UserCreateDTO);
    }
}
