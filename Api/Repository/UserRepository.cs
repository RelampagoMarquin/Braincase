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

        async Task<User> IUserRepository.CreateUser(CreateUserDTO createUserDTO)
        {
            // var user = _mapper.Map<User>(createUserDTO);
            var user = new User
            {
                Name = UserCreateDTO.Name,
                Email = UserCreateDTO.Email,
                Registration = UserCreateDTO.Registration,
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

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

    }
}
