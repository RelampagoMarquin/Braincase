using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using Api.Repository.Interfaces;
using Api.Dto.Institution;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionRepository _institutionRepository;

        public InstitutionController(IInstitutionRepository institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        // GET: api/Institution
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionResponseDTO>>> GetInstitution()
        {
            var institutions = await _institutionRepository.GetAllInstitutions();
            var resposeInstitutions = new List<InstitutionResponseDTO>();
            foreach (var institution in institutions)
            {
                InstitutionResponseDTO responseDTO = new InstitutionResponseDTO
                {
                    Id = institution.Id,
                    Name = institution.Name
                };
                resposeInstitutions.Add(responseDTO);
            }
            return resposeInstitutions;
        }

        // GET: api/Institution/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionResponseDTO>> GetInstitution(Guid id)
        {
            var institution = await _institutionRepository.GetInstitutionById(id);
            if (institution == null)
            {
                return NotFound("Instituição não encontrada");
            }
            var reponse = new InstitutionResponseDTO
            {
                    Id = institution.Id,
                    Name = institution.Name
            };
            return reponse;
        }

        // PUT: api/Institution/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitution(Guid id, InstitutionDTO institutionDTO)
        {
            var institution = await _institutionRepository.GetInstitutionById(id);
            if (institution == null)
            {
                return NotFound("Instituição não encontrada");
            }
            if(institutionDTO.Name != null)
            {
                institution.Name = institutionDTO.Name;
            }
            
            _institutionRepository.Update(institution);
            
            return await _institutionRepository.SaveChangesAsync()
                ? Ok("Instituição Atualizado com sucesso")
                : BadRequest("Erro ao atualizar a Instituição");
        }

        // POST: api/Institution
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostInstitution(InstitutionDTO InstitutionDTO)
        {
            var institution = new Institution
            {
                Name = InstitutionDTO.Name
            };

            _institutionRepository.Add(institution);

            return await _institutionRepository.SaveChangesAsync()
                ? Ok("Instituição criada com sucesso")
                : BadRequest("Erro ao criar a Instituição");
        }

        // DELETE: api/Institution/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitution(Guid id)
        {
            var institution = await _institutionRepository.GetInstitutionById(id);
            if (institution == null)
            {
                return NotFound("Instituição não encontrada");
            }

            _institutionRepository.Delete(institution);

            return await _institutionRepository.SaveChangesAsync()
                ? Ok("Instituição deletada com sucesso")
                : BadRequest("Erro ao deletar a Instituição");
        }
    }
}
