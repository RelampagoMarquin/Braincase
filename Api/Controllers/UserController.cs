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
        [HttpGet("{id}")]
        public async Task<ActionResult<ResposeUserDTO>> GetUser(Guid id)
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UpdateUserDTO userUpdateDTO)
        {
            var userBanco = await _userRepository.GetUserById(id);
            if (userBanco == null)
            {
                return NotFound("Usuário não encontrado");
            }

            var userUpdate = _mapper.Map(userUpdateDTO, userBanco);
            _userRepository.Update(userUpdate);

            return await _userRepository.SaveChangesAsync() 
                ? Ok("Usuário Atualizado com sucesso")
                : BadRequest("Erro ao atualizar o Usuário");
        }
        
        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResposeUserDTO>> PostUser(CreateUserDTO createUserDTO)
        {
            if (createUserDTO.Password != createUserDTO.ConfirmedPassword) {
                return BadRequest("Senhas diferentes");
            }
            createUserDTO.Password = PasswordHasher.HashPassword(createUserDTO.Password);
            var createdUser = await _userRepository.CreateUser(createUserDTO);
            var reponseDTO = _mapper.Map<ResposeUserDTO>(createdUser);
            return CreatedAtAction("GetUser", new { id = createdUser.Id }, reponseDTO);
          
        }
        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync()
                ? Ok("Usuário deletado com sucesso")
                : BadRequest("Erro ao deletar o Usuário");
        }

    }
}
