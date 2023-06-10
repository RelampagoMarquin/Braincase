using Api.Data;
using Api.Models;
using Api.Repository;
using Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Api.Dto.User;
using AutoMapper;

namespace Api.Repositorys
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(APIDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        async Task<User?> IUserRepository.GetUserById(Guid id)
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
            var user = _mapper.Map<User>(createUserDTO);

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
