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
using Api.Dto.Tag;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseTagDTO>>> GetTag()
        {
            var tags = await _tagRepository.GetAllTags();
            var responseTags = new List<ResponseTagDTO>();
            foreach (var tag in tags)
            {
                ResponseTagDTO responseDTO = new ResponseTagDTO
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    SubjectId = tag.Subject.Id
                };
                responseTags.Add(responseDTO);
            }
            return responseTags;
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseTagDTO>> GetTag(Guid id)
        {
            var tag = await _tagRepository.GetTagById(id);
            if (tag == null)
            {
                return NotFound("Tag não encontrada");
            }
            var response = new ResponseTagDTO
            {
                    Id = tag.Id,
                    Name = tag.Name,
                    SubjectId = tag.Subject.Id
            };
            return response;
        }

        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(Guid id, UpdateTagDTO updateTagDTO)
        {
            var tag = await _tagRepository.GetTagById(id);
            if (tag == null)
            {
                return NotFound("Tag não encontrada");
            }
            if(updateTagDTO.Name != null)
            {
                tag.Name = updateTagDTO.Name;
            }
            if(updateTagDTO.SubjectId != null)
            {
                tag.Subject.Id = (Guid)updateTagDTO.SubjectId;
            }

            _tagRepository.Update(tag);

            return await _tagRepository.SaveChangesAsync()
                ? Ok("Tag Atualizado com sucesso")
                : BadRequest("Erro ao atualizar a Tag");
        }

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostTag(CreateTagDTO createTagDTO)
        {
            var tag = new Tag
            {
                Name = createTagDTO.Name,
                SubjectId = createTagDTO.SubjectId,
            };

            _tagRepository.Add(tag);

            return await _tagRepository.SaveChangesAsync()
                ? Ok("Instituição criada com sucesso")
                : BadRequest("Erro ao criar a Instituição");

        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            return NoContent();
        }

    }
}
