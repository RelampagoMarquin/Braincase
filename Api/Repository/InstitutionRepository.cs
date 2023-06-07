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
    public class InstitutionRepository : BaseRepository, IInstitutionRepository
    {
        private readonly APIDbContext _context;
        public InstitutionRepository(APIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Institution>> GetAllInstitutions()
        {
            return await _context.Institution.ToListAsync();
        }

        async Task<Institution?> IInstitutionRepository.GetInstitutionById(Guid id)
        {
            var institution = await _context.Institution.FindAsync(id);

            if (institution is null)
            {
                return null;
            }
            return institution;
        }
    }
}