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
using Api.Dto.Comment;
using AutoMapper;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;

        }

        // GET: api/Comment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseCommentDTO>>> GetComment()
        {
          var comments = await _commentRepository.GetAllComments();
          var responseComments = new List<ResponseCommentDTO>();
          foreach (var comment in comments)
          {
            var response = _mapper.Map<ResponseCommentDTO>(comment);
            responseComments.Add(response);
          } 
          return responseComments;
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseCommentDTO>> GetCommentById(Guid id)
        {
          var comment = await _commentRepository.GetCommentById(id);
          if(comment == null)
          {
            return NotFound("Comentário não encontrado");
          }
          var response = _mapper.Map<ResponseCommentDTO>(comment);
          return response;
        }

        // PUT: api/Comment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(Guid id, CommentDTO commentDTO)
        {
            var commentBanco = await _commentRepository.GetCommentById(id);
            if(commentBanco == null) 
            {
                return NotFound("Comentário não encontrado");
            }
            
            var commentUpdate = _mapper.Map(commentDTO, commentBanco);

            _commentRepository.Update(commentUpdate);

            return await _commentRepository.SaveChangesAsync()
            ? Ok("Comentário atualizado com sucesso")
            : BadRequest("Erro ao atualizar o comentário");
        }

        // POST: api/Comment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostComment(CreateCommentDTO createCommentDTO)
        {
            var comment = _mapper.Map<Comment>(createCommentDTO);

            _commentRepository.Add(comment);

            return await _commentRepository.SaveChangesAsync()
                ? Ok("Comentário criado com sucesso")
                : BadRequest("Erro ao criar a Comentário");
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
        var comment = await _commentRepository.GetCommentById(id);
        if (comment == null)
        {
            return NotFound("Comentário não encontrada");
        }
        _commentRepository.Delete(comment);

        return await _commentRepository.SaveChangesAsync()
        ? Ok("Comentário deletada com sucesso")
        : BadRequest("Erro ao deletar a Comentário");
        }

    }
}
