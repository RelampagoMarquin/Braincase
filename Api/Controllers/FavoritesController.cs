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
using Api.Repository.Interfaces;
using Api.Dto.Favorites;
using AutoMapper;
using System.Security.Claims;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;

        public FavoritesController(IFavoriteRepository favoriteRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseFavoritesDTO>>> GetFavorites()
        {
            var favorites = await _favoriteRepository.GetAllFavorites();
            var responseFavorites = new List<ResponseFavoritesDTO>();
            foreach (var favorite in favorites)
            {
                var response = _mapper.Map<ResponseFavoritesDTO>(favorite);
                responseFavorites.Add(response);
            }
            return responseFavorites;
        }

        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<ResponseFavoritesDTO>>> GetFavoritesByUser()
        {
            var favorites = await _favoriteRepository.GetAllFavorites();
            var responseFavorites = new List<ResponseFavoritesDTO>();
            foreach (var favorite in favorites)
            {
                var response = _mapper.Map<ResponseFavoritesDTO>(favorite);
                responseFavorites.Add(response);
            }
            return responseFavorites;
        }

        // GET: api/Favorites/5
        [HttpGet("{QuestionId}")]
        public async Task<ActionResult<ResponseFavoritesDTO>> GetFavoritesById(Guid QuestionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorite = await _favoriteRepository.GetFavoritesById(userId, QuestionId);
            if(favorite == null)
            {
                return NotFound("Favorito não encontrada");
            }
            var response = _mapper.Map<ResponseFavoritesDTO>(favorite);
            return response;
        }

        // POST: api/Favorites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostFavorites(FavoritesDTO favoritesDTO)
        {
            favoritesDTO.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorite = _mapper.Map<Favorites>(favoritesDTO);

            _favoriteRepository.Add(favorite);

            return await _favoriteRepository.SaveChangesAsync()
                ? Ok("Favorito adicionado com sucesso")
                : BadRequest("Erro ao adicionar aos Favoritos");
        }

        // DELETE: api/Favorites/5
        [HttpDelete("{QuestionId}")]
        public async Task<IActionResult> DeleteFavorites(Guid QuestionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var farovite = await _favoriteRepository.GetFavoritesById(userId, QuestionId);
            if(farovite == null)
            {
                return NotFound("Favorito não encontrado");
            }

            _favoriteRepository.Delete(farovite);

            return await _favoriteRepository.SaveChangesAsync()
                ? Ok("Favorito deletado com sucesso")
                : BadRequest("Erro ao deletar o Favorito");
        }

    }
}
