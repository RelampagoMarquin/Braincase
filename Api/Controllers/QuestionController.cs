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
using Api.Dto.Question;
using AutoMapper;
using Api.Repository.Interfaces;
using System.Security.Claims;

namespace Api.Controllers
{
    [Authorize]
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

        [HttpGet("public")]
        public async Task<ActionResult<IEnumerable<ResponseQuestionDTO>>> GetAllPublic()
        {
            var questions = await _questionRepository.GetAllPublic();
            var responseQuestions = new List<ResponseQuestionDTO>();
            foreach (var question in questions)
            {
                var response = _mapper.Map<ResponseQuestionDTO>(question);
                responseQuestions.Add(response);
            }
            return responseQuestions;
        }

        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<ResponseQuestionDTO>>> GetMyQuestions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("Id null");
            }
            var questions = await _questionRepository.GetMyQuestions(userId);
            var responseQuestions = new List<ResponseQuestionDTO>();
            foreach (var question in questions)
            {
                var response = _mapper.Map<ResponseQuestionDTO>(question);
                responseQuestions.Add(response);
            }
            return responseQuestions;
        }

        [HttpGet("user/favorites")]
        public async Task<ActionResult<IEnumerable<ResponseQuestionDTO>>> GetMyFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("Id null");
            }
            var questions = await _questionRepository.GetMyFavorites(userId);
            var responseQuestions = new List<ResponseQuestionDTO>();
            foreach (var question in questions)
            {
                var response = _mapper.Map<ResponseQuestionDTO>(question);
                responseQuestions.Add(response);
            }
            return responseQuestions;
        }

        // este get pega todas as questões publicas e soma com as privadas do usuário
        [HttpGet("user/all")]
        public async Task<ActionResult<IEnumerable<ResponseQuestionDTO>>> GetAllUserQuestion()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("Id null");
            }
            var questions = await _questionRepository.GetAllUserQuestion(userId);
            var responseQuestions = new List<ResponseQuestionDTO>();
            foreach (var question in questions)
            {
                var response = _mapper.Map<ResponseQuestionDTO>(question);
                responseQuestions.Add(response);
            }
            return responseQuestions;
        }

        // PUT: api/Question/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseQuestionDTO>> PutQuestion(Guid id, UpdateQuestionDTO updateQuestionDTO)
        {
            var question = await _questionRepository.GetQuestionById(id);
            if(question == null)
            {
                return NotFound("Questão não encontrada");
            }
            // var questionUpdate = _mapper.Map(updateQuestionDTO, question);
            
            question = await _questionRepository.UpdateQuestion(updateQuestionDTO, question);

            var response = _mapper.Map<ResponseQuestionDTO>(question);
            return response;
        }

        // POST: api/Question
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<Question> PostQuestion(CreateQuestionDTO createQuestionDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("Id null"); 
            }
            var question = await _questionRepository.CreateQuestion(createQuestionDTO, userId);

            return question;
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
