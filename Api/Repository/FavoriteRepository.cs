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
    public class FavoriteRepository : BaseRepository, IFavoriteRepository
    {
        private readonly APIDbContext _context;

        public FavoriteRepository(APIDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Favorites>> GetAllFavorites()
        {
            // user e question
            return await _context.Favorites.ToListAsync();
        }

        public async Task<Favorites?> GetFavoritesById(String UserId, Guid QuestionId)
        {
            var favorite = await _context.Favorites.FirstOrDefaultAsync(x => x.UserId == UserId && x.QuestionId == QuestionId );
            if(favorite is null)
            {
                return null;
            }
            return favorite;
        }
    }
}