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
using Api.Dto.Subject;
using AutoMapper;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SubjectController : ControllerBase
    {
        
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseSubjectDTO>>> GetSubject()
        {
            var subjects = await _subjectRepository.GetAllSubject();
            var responseSubjects = new List<ResponseSubjectDTO>();
            foreach (var subject in subjects) 
            {
                var response = _mapper.Map<ResponseSubjectDTO>(subject);
                responseSubjects.Add(response);  
            }
            return responseSubjects;
        }

        // GET: api/Subject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseSubjectDTO>> GetSubjectById(Guid id)
        {
            var subject =  await _subjectRepository.GetSubjectById(id);
            if (subject == null) 
            {
                return NotFound();
            }

            var response = _mapper.Map<ResponseSubjectDTO>(subject);

            return response;
        }

        // PUT: api/Subject/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(Guid id, SubjectDTO subjectDTO)
        {
            var subjectBanco = await _subjectRepository.GetSubjectById(id);
            if (subjectBanco == null)
            {
                return NotFound("Instituição não encontrada");
            }

            var subjectUpdate = _mapper.Map(subjectDTO, subjectBanco);

            _subjectRepository.Update(subjectUpdate);

            return await _subjectRepository.SaveChangesAsync()
                ? Ok("Matéria foi atualizada")
                : BadRequest("Erro ao atualizar a matéria");
              
        }

        // POST: api/Subject
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostSubject(SubjectDTO SubjectDTO)
        {
            var subject = _mapper.Map<Subject>(SubjectDTO);

            _subjectRepository.Add(subject);

            return await _subjectRepository.SaveChangesAsync()
                ? Ok("Matéria criada com sucesso")
                : BadRequest("Erro ao criar a matéria");

        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var subject = await _subjectRepository.GetSubjectById(id);
            if(subject == null) 
            {
                return NotFound("Não foi possível encontrar a matéria.");
            }
            _subjectRepository.Delete(subject);
            return await _subjectRepository.SaveChangesAsync()
            ? Ok("Matéria deletada com sucesso")
            : BadRequest("Erro ao deletar matéria");
        }

    }
}
