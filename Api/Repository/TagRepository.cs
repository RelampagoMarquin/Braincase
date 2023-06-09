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
    public class TagRepository: BaseRepository, ITagRepository
    {
        private readonly APIDbContext _context;

        public TagRepository(APIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _context.Tag.Include(x => x.Subject).ToListAsync();
        }

        public async Task<Tag?> GetTagById(Guid id)
        {
            var tag = await _context.Tag.Include(x => x.Subject).FirstOrDefaultAsync(x => x.Id == id);

            if (tag is null)
            {
                return null;
            }
            return tag;
        }
        
    }
}