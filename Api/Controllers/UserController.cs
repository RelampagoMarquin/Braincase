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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUser()
        {
            var users = await _userRepository.GetAllUsers();
            var responseUsers = new List<UserResponseDTO>();
            foreach (var user in users)
            {
              UserResponseDTO reponseDTO = new UserResponseDTO
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Registration = user.Registration,
            };
            responseUsers.Add(reponseDTO);  
            }
            return responseUsers;
        }

        // GET: api/User/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser(String id)
        {

            var user = await _userRepository.GetUserById(id);
            
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }
            var response = new UserResponseDTO
            {
                Registration = user.Registration,
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
            };
            return response;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(String id, UserUpdateDTO userUpdateDTO)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            if(userUpdateDTO.Name != null && userUpdateDTO.Name != user.Name)
            {
                user.Name = userUpdateDTO.Name;
            }
            if (userUpdateDTO.Registration != null && userUpdateDTO.Registration != user.Registration)
            {
                user.Registration = userUpdateDTO.Registration;
            }
            if (userUpdateDTO.Email != null && userUpdateDTO.Email != user.Email)
            {
                user.Email = userUpdateDTO.Email;
            }
            if (userUpdateDTO.Password != null && userUpdateDTO.ConfirmedPassword != null )
            {
                if(userUpdateDTO.Password != userUpdateDTO.ConfirmedPassword)
                {
                    return BadRequest("Senhas diferentes");
                }
                await _userRepository.ChangePassword(user, userUpdateDTO.oldPassword, userUpdateDTO.Password);
            }

            var result = await _userRepository.UpdateUser(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Success = false, Message = "Erro ao alterar usuario" });
            }
            return Ok(new Response { Message = "Usuário Atualizado com sucesso!" });
        }


        // DELETE: api/User/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(String id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            var result = await _userRepository.DeleteUser(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Success = false, Message = "Erro ao deletar usuario" });
            }
            return Ok(new Response { Message = "Usuário Deletado com sucesso!" });
        }

    }
}
