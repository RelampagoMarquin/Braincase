using Api.Data;
using Api.Models;
using Api.Repository;
using Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Api.Dto.User;

namespace Api.Repositorys
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly APIDbContext _context;
        public UserRepository(APIDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        async Task<User?> IUserRepository.GetUserById(Guid id)
        {
            var user = await _context.User.FindAsync(id);

            if (user is null)
            {
                return null;
            }
            return user;
        }

        async Task<User> IUserRepository.CreateUser(UserCreateDTO UserCreateDTO)
        {
            var user = new User
            {
                Name = UserCreateDTO.Name,
                Email = UserCreateDTO.Email,
                Password = UserCreateDTO.Password,
                Registration = UserCreateDTO.Registration,
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
