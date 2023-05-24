using Api.Dto;
using Api.Models;
using Api.Repositorys;

namespace Api.Services
{
    public class UserService
    {
        /* private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<User> CreateUser(CreateUserDTO createUserDTO)
        {
            // Aqui você pode adicionar a lógica de criação do usuário
            // Por exemplo, pode fazer a validação dos dados, gerar um ID único, etc.

            // Crie uma instância da entidade User e atribua os valores do DTO
            var user = new User
            {
                Name = createUserDTO.Name,
                Email = createUserDTO.Email,
                Password = createUserDTO.Password
            };

            // Salve o usuário no repositório
            await _userRepository.CreateUser(user);

            return user;
        }

        public async Task<User> UpdateUser(Guid id, User user)
        {
            // Verifique se o usuário existe no repositório antes de atualizá-lo
            if (await _userRepository.UserExists(id))
            {
                user.Id = id; // Certifique-se de definir o ID do usuário corretamente
                await _userRepository.UpdateUser(user);
                return user;
            }

            return null;
        }

        public async Task<User> DeleteUser(Guid id)
        {
            // Verifique se o usuário existe no repositório antes de excluí-lo
            var user = await _userRepository.GetUser(id);
            if (user != null)
            {
                await _userRepository.DeleteUser(user);
                return user;
            }

            return null;
        }
        */
    }
}
