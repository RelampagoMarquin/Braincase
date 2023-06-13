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
    public class SubjectRepository : BaseRepository, ISubjectRepository
    {
        private readonly APIDbContext _context;
        public SubjectRepository(APIDbContext context) : base(context)
        {
            _context = context;
        }
    public async Task<IEnumerable<Subject>> GetAllSubject()
    {
        return await _context.Subject.ToListAsync();
    }

    async Task<Subject?> ISubjectRepository.GetSubjectById(System.Guid id){
        var subject = await _context.Subject.FindAsync(id);
        if (subject is null)
        {
            return null;
        }
        return subject;
    }
    }
}