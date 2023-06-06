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
using Api.Repository.Interfaces;
using Api.utils;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUser()
        {
            var users = await _userRepository.GetAllUsers();
            var responseUsers = new List<UserResponseDTO>();
            foreach (var user in users)
            {
              UserResponseDTO reponseDTO = new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Registration = user.Registration,
            };
            responseUsers.Add(reponseDTO);  
            }
            return responseUsers;
        }

       // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser(Guid id)
        {

            var user = await _userRepository.GetUserById(id);
            
            if (user == null)
            {
                return NotFound();
            }
            var reponse = new UserResponseDTO
            {
                Registration = user.Registration,
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
            };
            return reponse;
        }
        
        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserUpdateDTO userUpdateDTO)
        {
            var user = await _userRepository.GetUserById(id);
            if(userUpdateDTO.Name != null)
            {
                user.Name = userUpdateDTO.Name;
            }
            if (userUpdateDTO.Registration != null)
            {
                user.Registration = userUpdateDTO.Registration;
            }
            if (userUpdateDTO.Password != null && userUpdateDTO.ConfirmedPassword != null )
            {
                if(userUpdateDTO.Password != userUpdateDTO.ConfirmedPassword)
                {
                    return BadRequest("Senhas diferentes");
                }
                var haspass = PasswordHasher.HashPassword(userUpdateDTO.Password);
                user.Password = haspass;
            }
            if (userUpdateDTO.Email != null)
            {
                user.Email = userUpdateDTO.Email;
            }
            _userRepository.Update(user);

            return await _userRepository.SaveChangesAsync() 
                ? Ok("User Atualizado com sucesso")
                : BadRequest("Erro ao atualizar o User"); ;
        }

        
        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> PostUser(UserCreateDTO createUserDTO)
        {
            if (createUserDTO.Password != createUserDTO.ConfirmedPassword) {
                return BadRequest("Senhas diferentes");
            }
            createUserDTO.Password = PasswordHasher.HashPassword(createUserDTO.Password);
            var createdUser = await _userRepository.CreateUser(createUserDTO);
            UserResponseDTO reponseDTO = new UserResponseDTO
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email,
            };
            return CreatedAtAction("GetUser", new { id = createdUser.Id }, reponseDTO);
          
        }
        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("User não encontrado");
            }

            _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync()
                ? Ok("User deletado com sucesso")
                : BadRequest("Erro ao deletar o User");
        }

    }
}
