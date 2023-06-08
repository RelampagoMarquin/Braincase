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
namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SubjectController : ControllerBase
    {
        
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
           _subjectRepository = subjectRepository;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetSubject()
        {
         var subjects = await _subjectRepository.GetAllSubject();
         var responseSubjects = new List<SubjectDTO>();
         foreach (var subject in subjects) 
         {
            SubjectDTO responseDTO = new SubjectDTO
            {
                Name = subject.Name,
            };
            responseSubjects.Add(responseDTO);
         }
         return responseSubjects;
        }

        // GET: api/Subject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDTO>> GetSubject(Guid id)
        {
          var subject =  await _subjectRepository.GetSubjectById(id);
          if (subject == null) 
          {
            return NotFound();
          }
          var response = new SubjectDTO{
            Name = subject.Name,
          };
          return response;
        }

        // PUT: api/Subject/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(Guid id, SubjectDTO subjectDTO)
        {
            var subject = await _subjectRepository.GetSubjectById(id);
            
            if (subject == null)
            {
                return NotFound("Matéria não encontrado");
            }
            if (subjectDTO.Name != null)
                {
                    subject.Name = subjectDTO.Name;
                }
            _subjectRepository.Update(subject);

            return await _subjectRepository.SaveChangesAsync()
                ? Ok("Matéria foi criada")
                : BadRequest("Erro ao atualizar a matéria");
              
        }

        // POST: api/Subject
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubjectDTO>> PostSubject(SubjectDTO subjectDTO)
        {
            _subjectRepository.Add(subjectDTO);

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
