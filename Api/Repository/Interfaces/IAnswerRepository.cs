using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface IAnswerRepository : IBaseRepository
    {
        Task<IEnumerable<Answer>> GetAllAnswers();

        Task<Answer?> GetAnswerById(Guid id);
    }
}