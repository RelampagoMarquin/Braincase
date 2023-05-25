using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using Api.Services;
using Api.Dto.User;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly APIDbContext _context;
        // private readonly UserService _userService;

        public UserController(APIDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUser()
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var users =  await _context.User.ToListAsync();
            var responseUsers = new List<UserResponseDTO>();
            foreach (var user in users)
            {
              UserResponseDTO reponseDTO = new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
            responseUsers.Add(reponseDTO);  
            }
            return responseUsers;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        /// <summary>
        /// Creates a User.
        /// </summary>
        /// <returns>A newly created User</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///     {
        ///        "name": "string",
        ///        "email": "string@dominio.com",
        ///        "password": "string",
        ///        "confirmedPassword": "string"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created User</response>
        /// <response code="400">If the user is null</response>
        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponseDTO>> PostUser(UserCreateDTO createUserDTO)
        {
          if (_context.User == null)
          {
              return Problem("Entity set 'APIDbContext.User'  is null.");
          }
            // var createdUser = await _userService. CreateUser(createUserDTO);

           //  return userService.createUser(createUserDTO);
          
            User user = new User
            {
                Name = createUserDTO.Name,
                Email = createUserDTO.Email,
                Password = createUserDTO.Password
            };
           
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            UserResponseDTO reponseDTO = new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
            return CreatedAtAction("GetUser", new { id = user.Id }, reponseDTO);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
