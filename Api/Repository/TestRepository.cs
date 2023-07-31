using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dto.Test;
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
            return await _context.Test.OrderByDescending(x => x.LastUse).ToListAsync();
        }

        async Task<Test?> ITestRepository.GetTestById(Guid id)
        {
            var test = await _context.Test
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (test is null)
            {
                return null;
            }
            return test;
        }

        public async Task<IEnumerable<Test>> GetAllTestsByUser(String userId)
        {
            return await _context.Test.Where(x => x.UserId == userId)
            .Include(x => x.Questions)
            .OrderByDescending(x => x.LastUse).ToListAsync();
        }

        public async Task<Test> CreateTest(CreateTestDTO createTestDTO, string userid)
        {
            Test test = new Test
            {
                ClassName = createTestDTO.ClassName,
                LastUse = DateTime.Now,
                UserId = userid,
                LogoUrl = createTestDTO.LogoUrl,
                Name = createTestDTO.Name,
                CreateAt = DateTime.Now,
            };

            _context.Add(test);

            await _context.SaveChangesAsync();

            return test;

        }

        public async Task<Test> AddQuestionToTest(AddQuestionTetstDTO addQuestionTetstDTO, Test test)
        {
            var listaRetirar = new List<Question>();
            foreach (var question in test.Questions){
                if(addQuestionTetstDTO.Questions.Find(item => item.Id == question.Id) == null){
                    listaRetirar.Add(question);
                }
            }

            foreach (var question in listaRetirar){
                test.Questions.Remove(question);
            }

            foreach (var question in addQuestionTetstDTO.Questions)
            {
                if (test.Questions.Find(item => item.Id == question.Id) == null)
                {
                    test.Questions.Add(question);
                }
            }

            _context.Update(test);
            await _context.SaveChangesAsync();
            return test;

            }
    }
}