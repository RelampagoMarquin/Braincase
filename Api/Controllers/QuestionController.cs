using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using Api.Dto.Question;
using AutoMapper;
using Api.Repository.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseQuestionDTO>>> GetQuestion()
        {
            var questions = await _questionRepository.GetAllQuestions();
            var responseQuestions = new List<ResponseQuestionDTO>();
            foreach (var question in questions)
            {
                var response = _mapper.Map<ResponseQuestionDTO>(question);
                responseQuestions.Add(response);
            }
            return responseQuestions;
        }

        // GET: api/Question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseQuestionDTO>> GetQuestionById(Guid id)
        {
          var question = await _questionRepository.GetQuestionById(id);
          if(question == null)
          {
            return NotFound("Questão não encontrada");
          }
            var response = _mapper.Map<ResponseQuestionDTO>(question);
            return response;
        }

        // PUT: api/Question/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(Guid id, UpdateQuestionDTO updateQuestionDTO)
        {
            var questionBanco = await _questionRepository.GetQuestionById(id);
            if(questionBanco == null)
            {
                return NotFound("Questão não encontrada");
            }
            var questionUpdate = _mapper.Map(updateQuestionDTO, questionBanco);
            _questionRepository.Update(questionUpdate);

            return await _questionRepository.SaveChangesAsync()
            ? Ok("Questão atualizada com sucesso")
            : BadRequest("Não foi possível atualizar a questão");
        }

        // POST: api/Question
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostQuestion(CreateQuestionDTO createQuestionDTO)
        {
            var question = _mapper.Map<Question>(createQuestionDTO);

            _questionRepository.Add(question);

            return await _questionRepository.SaveChangesAsync()
            ? Ok("Questão criada com Sucesso")
            : BadRequest("Erro ao criar a Questão");
        }

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            var question = await _questionRepository.GetQuestionById(id);
            if(question == null)
            {
                return NotFound("Questão não encontrada");
            }
            _questionRepository.Delete(question);

            return await _questionRepository.SaveChangesAsync()
            ? Ok("Questão deletada com sucesso")
            : BadRequest("Erro ao deletar a questão");
        }

    }
}
