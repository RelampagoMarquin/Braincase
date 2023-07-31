using Api.Data;
using Api.Models;
using Api.Repository;
using Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Api.Dto.User;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace Api.Repositorys
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly APIDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(APIDbContext context, UserManager<User> userManager, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        async Task<User?> IUserRepository.GetUserById(String id)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                return null;
            }
            return user;
        }

        async Task<IdentityResult> IUserRepository.UpdateUser(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        async Task<IdentityResult> IUserRepository.ChangePassword(User user, String oldPassword, String password)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, password);
            return result;
        }


        async Task<IdentityResult> IUserRepository.DeleteUser(User user)
        {
            
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        async public Task<User?> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        async public Task<IdentityResult> AddUser(CreateUserDTO createUserDTO)
        {
            // craiação do username
            // Remova espaços em branco do início e do final do nome
            var name = createUserDTO.Name.Trim();

            // Remova caracteres especiais do nome
            var validCharacters = new HashSet<char>("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.-_+@");
            var sanitizedName = new string(name.Where(c => validCharacters.Contains(c)).ToArray());
            var random = new Random();
            var randomChars = new char[5];

            for (int i = 0; i < randomChars.Length; i++)
            {
                randomChars[i] = (char)random.Next(97, 123); // Gera um caractere aleatório minúsculo (97 a 122 na tabela ASCII)
            }

            var finalName = sanitizedName + new string(randomChars);
            User user = new()
            {
                UserName = finalName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = createUserDTO.Email,
                Name = createUserDTO.Name,
                Registration = createUserDTO.Registration,

            };

            var result = await _userManager.CreateAsync(user, createUserDTO.Password);

            return result;
        }

        async public Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
