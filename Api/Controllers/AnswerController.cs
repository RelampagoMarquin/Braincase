using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Api.Repository.Interfaces;
using Api.Dto.Answer;
using AutoMapper;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswerController(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        // GET: api/Answer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseAnswerDTO>>> GetAnswer()
        {
          var answers = await _answerRepository.GetAllAnswers();
          var responseAnswer = new List<ResponseAnswerDTO>();
          foreach(var answer in answers)
          {
            var response = _mapper.Map<ResponseAnswerDTO>(answer);
            responseAnswer.Add(response);
          }
          return responseAnswer;
        }

        // GET: api/Answer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAnswerDTO>> GetAnswerById(Guid id)
        {
          var answer = await _answerRepository.GetAnswerById(id);
          if(answer == null)
          {
            return NotFound("Resposta não encontrada");
          }
          var response = _mapper.Map<ResponseAnswerDTO>(answer);
          return response;
        }

        // PUT: api/Answer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(Guid id, UpdateAnswerDTO updateAnswerDTO)
        {
           var answerBanco = await _answerRepository.GetAnswerById(id);
           if(answerBanco == null)
           {
            return NotFound("Resposta não encontrada");
           }

           var answerUpdate = _mapper.Map(updateAnswerDTO, answerBanco);
           _answerRepository.Update(answerUpdate);

           return await _answerRepository.SaveChangesAsync()
                ? Ok("Resposta Atualizado com sucesso")
                : BadRequest("Erro ao atualizar a Resposta");
        }

        // POST: api/Answer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostAnswer(CreateAnswerDTO createAnswerDTO)
        {
            var answer = _mapper.Map<Answer>(createAnswerDTO);

            _answerRepository.Add(answer);

            return await _answerRepository.SaveChangesAsync()
                ? Ok("Resposta criada com sucesso")
                : BadRequest("Erro ao criar a Resposta");
        }

        // DELETE: api/Answer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(Guid id)
        {
            var answer = await _answerRepository.GetAnswerById(id);
            if(answer == null)
            {
                return NotFound("Resposta não encontrada");
            }

            _answerRepository.Delete(answer);

            return await _answerRepository.SaveChangesAsync()
                ? Ok("Resposta deletada com sucesso")
                : BadRequest("Erro ao deletar a Resposta");
        }

        
    }
}
