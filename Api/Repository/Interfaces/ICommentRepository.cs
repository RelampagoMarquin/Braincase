using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface ICommentRepository : IBaseRepository
    {
        Task<IEnumerable<Comment>> GetAllComments();

        Task<Comment?> GetCommentById(Guid id);
    }
}