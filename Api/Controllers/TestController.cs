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
using AutoMapper;
using Api.Repository.Interfaces;
using Api.Dto.Test;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestController(ITestRepository testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }

        // GET: api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseTestDTO>>> GetTest()
        {
            var tests = await _testRepository.GetAllTests();
            var responseTests = new List<ResponseTestDTO>();
            foreach (var test in tests)
            {
                var response = _mapper.Map<ResponseTestDTO>(test);
                responseTests.Add(response);
            }
            return responseTests;
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseTestDTO>> GetTestById(Guid id)
        {
            var test = await _testRepository.GetTestById(id);
            if (test == null)
            {
                return NotFound("Prova não encontrada");
            }
            var response = _mapper.Map<ResponseTestDTO>(test);
            return response;
        }

        [HttpGet("user{id}")]
        public async Task<ActionResult<IEnumerable<ResponseTestDTO>>> GetAllTestsByUser(String id)
        {
            var tests = await _testRepository.GetAllTestsByUser(id);
            var responseTests = new List<ResponseTestDTO>();
            foreach (var test in tests)
            {
                var response = _mapper.Map<ResponseTestDTO>(test);
                responseTests.Add(response);
            }
            return responseTests;
        }


        // PUT: api/Test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTest(Guid id, UpdateTestDTO updateTestDTO)
        {
            var testBanco = await _testRepository.GetTestById(id);
            if (testBanco == null)
            {
                return NotFound("Prova não encontrada");
            }
            var institutionUpdate = _mapper.Map(updateTestDTO, testBanco);
            
            _testRepository.Update(institutionUpdate);
            
            return await _testRepository.SaveChangesAsync()
                ? Ok("Prova Atualizada com sucesso")
                : BadRequest("Erro ao atualizar a Prova");
        }

        // POST: api/Test
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Test>> PostTest(CreateTestDTO createTestDTO)
        {
            var test = _mapper.Map<Test>(createTestDTO);

            _testRepository.Add(test);

            return await _testRepository.SaveChangesAsync()
                ? Ok("Prova criada com sucesso")
                : BadRequest("Erro ao criar a Prova");
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            var test = await _testRepository.GetTestById(id);
            if (test == null)
            {
                return NotFound("Prova não encontrada");
            }

            _testRepository.Delete(test);

            return await _testRepository.SaveChangesAsync()
                ? Ok("Prova deletada com sucesso")
                : BadRequest("Erro ao deletar a Prova");
        }
    }
}
