using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using Api.Repository.Interfaces;

namespace Api.Repository
{
    public class CommentRepository: BaseRepository, ICommentRepository
    {
        private readonly APIDbContext _context;

        public CommentRepository(APIDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            // user e question
            return await _context.Comment.Include(x =>x.User).ToListAsync();
        }

        public async Task<Comment?> GetCommentById(Guid id)
        {
            var comment = await _context.Comment.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

            if(comment is null)
            {
                return null;
            }
            return comment;
        } 
    }
}