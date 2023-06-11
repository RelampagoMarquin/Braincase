using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly APIDbContext _context;

        public QuestionController(APIDbContext context)
        {
            _context = context;
        }

        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestion()
        {
          if (_context.Question == null)
          {
              return NotFound();
          }
            return await _context.Question.ToListAsync();
        }

        // GET: api/Question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestionById(Guid id)
        {
          if (_context.Question == null)
          {
              return NotFound();
          }
            var question = await _context.Question.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Question/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(Guid id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Question
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
          if (_context.Question == null)
          {
              return Problem("Entity set 'APIDbContext.Question'  is null.");
          }
            _context.Question.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            if (_context.Question == null)
            {
                return NotFound();
            }
            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Question.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(Guid id)
        {
            return (_context.Question?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
