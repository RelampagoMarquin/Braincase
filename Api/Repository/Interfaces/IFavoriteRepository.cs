using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IFavoriteRepository : IBaseRepository
    {
        Task<IEnumerable<Favorites>> GetAllFavorites();

        Task<Favorites?> GetFavoritesById(String UserId, Guid QuestionId);
    }
}