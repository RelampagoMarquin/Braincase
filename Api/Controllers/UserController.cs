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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/User
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResposeUserDTO>>> GetUser()
        {
            var users = await _userRepository.GetAllUsers();
            var responseUsers = new List<ResposeUserDTO>();
            foreach (var user in users)
            {
                var response = _mapper.Map<ResposeUserDTO>(user);
                responseUsers.Add(response);
            }
            return responseUsers;
        }

        // GET: api/User/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResposeUserDTO>> GetUser(String id)
        {

            var user = await _userRepository.GetUserById(id);
            
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }
            var response = _mapper.Map<ResposeUserDTO>(user);
            return response;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserById(String id, UpdateUserDTO userUpdateDTO)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }
            // var userUpdate = _mapper.Map(userUpdateDTO, userBanco);
            // _userRepository.Update(userUpdate);
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
