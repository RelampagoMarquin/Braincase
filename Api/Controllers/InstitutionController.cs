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
using Microsoft.AspNetCore.Authorization;
﻿using AutoMapper;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IMapper _mapper;

        public InstitutionController(IInstitutionRepository institutionRepository, IMapper mapper)
        {
            _institutionRepository = institutionRepository;
            _mapper = mapper;
        }

        // GET: api/Institution
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseInstitutionDTO>>> GetInstitution()
        {
            var institutions = await _institutionRepository.GetAllInstitutions();
            var resposeInstitutions = new List<ResponseInstitutionDTO>();
            foreach (var institution in institutions)
            {
                var response = _mapper.Map<ResponseInstitutionDTO>(institution);
                resposeInstitutions.Add(response);
            }
            return resposeInstitutions;
        }

        // GET: api/Institution/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseInstitutionDTO>> GetInstitutionById(Guid id)
        {
            var institution = await _institutionRepository.GetInstitutionById(id);
            if (institution == null)
            {
                return NotFound("Instituição não encontrada");
            }
            var response = _mapper.Map<ResponseInstitutionDTO>(institution);
            return response;
        }

        // PUT: api/Institution/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitution(Guid id, InstitutionDTO institutionDTO)
        {
            var institutionBanco = await _institutionRepository.GetInstitutionById(id);
            if (institutionBanco == null)
            {
                return NotFound("Instituição não encontrada");
            }
            var institutionUpdate = _mapper.Map(institutionDTO, institutionBanco);
            
            _institutionRepository.Update(institutionUpdate);
            
            return await _institutionRepository.SaveChangesAsync()
                ? Ok("Instituição Atualizado com sucesso")
                : BadRequest("Erro ao atualizar a Instituição");
        }

        // POST: api/Institution
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostInstitution(InstitutionDTO InstitutionDTO)
        {
            var institution = _mapper.Map<Institution>(InstitutionDTO);

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
