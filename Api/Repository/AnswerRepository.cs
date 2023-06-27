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
    public class AnswerRepository : BaseRepository, IAnswerRepository
    {
        private readonly APIDbContext _context;
        public AnswerRepository(APIDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Answer>> GetAllAnswers()
        {
            return await _context.Answer.ToListAsync();
        }

        public async Task<Answer?> GetAnswerById(Guid id)
        {
            var answer = await _context.Answer.FirstOrDefaultAsync(x => x.Id == id);
            
            if (answer is null)
            {
                return null;
            }
            return answer;
        }
    }
}