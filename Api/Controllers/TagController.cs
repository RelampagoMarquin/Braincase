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
﻿using AutoMapper;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseTagDTO>>> GetTag()
        {
            var tags = await _tagRepository.GetAllTags();
            var responseTags = new List<ResponseTagDTO>();
            foreach (var tag in tags)
            {
                var response = _mapper.Map<ResponseTagDTO>(tag);
                responseTags.Add(response);   
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
            var response = _mapper.Map<ResponseTagDTO>(tag);
            return response;
        }

        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(Guid id, UpdateTagDTO updateTagDTO)
        {
            var tagBanco = await _tagRepository.GetTagById(id);
            if (tagBanco == null)
            {
                return NotFound("Tag não encontrada");
            }

            var tagUpdate = _mapper.Map(updateTagDTO, tagBanco);

            _tagRepository.Update(tagUpdate);

            return await _tagRepository.SaveChangesAsync()
                ? Ok("Tag Atualizado com sucesso")
                : BadRequest("Erro ao atualizar a Tag");
        }

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostTag(CreateTagDTO createTagDTO)
        {
            var tag = _mapper.Map<Tag>(createTagDTO);

            _tagRepository.Add(tag);

            return await _tagRepository.SaveChangesAsync()
                ? Ok("Tag criada com sucesso")
                : BadRequest("Erro ao criar a Tag");

        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var tag = await _tagRepository.GetTagById(id);
            if (tag == null)
            {
                return NotFound("Tag não encontrada");
            }

            _tagRepository.Delete(tag);

            return await _tagRepository.SaveChangesAsync()
                ? Ok("Tag deletada com sucesso")
                : BadRequest("Erro ao deletar a Tag");
        }
    }
}
