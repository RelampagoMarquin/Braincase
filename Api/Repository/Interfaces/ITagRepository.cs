using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repository.Interfaces
{
    public interface ITagRepository: IBaseRepository
    {
        Task<IEnumerable<Tag>> GetAllTags();

        Task<Tag?> GetTagById(Guid id);

        Task<IEnumerable<Tag>> GetTagBySubjectId(Guid subjectId);
    }
}