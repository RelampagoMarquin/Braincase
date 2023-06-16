using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class TestRepository : BaseRepository, ITestRepository
    {
        private readonly APIDbContext _context;

        public TestRepository(APIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Test>> GetAllTests()
        {
            return await _context.Test.ToListAsync();
        }

        async Task<Test?> ITestRepository.GetTestById(Guid id)
        {
            var test = await _context.Test.FirstOrDefaultAsync(x => x.Id == id);

            if (test is null)
            {
                return null;
            }
            return test;
        }

        public async Task<IEnumerable<Test>> GetAllTestsByUser(String userId)
        {
            return await _context.Test.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}