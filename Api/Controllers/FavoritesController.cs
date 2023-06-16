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

        // GET: api/Favorites/5
        [HttpGet("{UserId},{QuestionId}")]
        public async Task<ActionResult<ResponseFavoritesDTO>> GetFavoritesById(Guid UserId, Guid QuestionId)
        {
            var favorite = await _favoriteRepository.GetFavoritesById(UserId, QuestionId);
            if(favorite == null)
            {
                return NotFound("Favorito não encontrada");
            }
            var response = _mapper.Map<ResponseFavoritesDTO>(favorite);
            return response;
        }

        // PUT: api/Favorites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutFavorites(Guid id, FavoritesDTO favoritesDTO)
        // {
        //     var favoriteBanco = await _favoriteRepository.GetFavoritesById(id);
        //     if(favoriteBanco == null)
        //     {
        //         return NotFound("Favorito não encontrado");
        //     }

        //     var favoriteUpdate = _mapper.Map(favoritesDTO, favoriteBanco);
            
        //     _favoriteRepository.Update(favoriteUpdate);

        //     return await _favoriteRepository.SaveChangesAsync()
        //         ? Ok("Favorito Atualizado com sucesso")
        //         : BadRequest("Erro ao atualizar o Favoritado");
        // }

        // POST: api/Favorites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostFavorites(FavoritesDTO favoritesDTO)
        {
            var favorite = _mapper.Map<Favorites>(favoritesDTO);

            _favoriteRepository.Add(favorite);

            return await _favoriteRepository.SaveChangesAsync()
                ? Ok("Favorito adicionado com sucesso")
                : BadRequest("Erro ao adicionar aos Favoritos");
        }

        // DELETE: api/Favorites/5
        [HttpDelete("{UserId},{QuestionId}")]
        public async Task<IActionResult> DeleteFavorites(Guid UserId, Guid QuestionId)
        {
            var farovite = await _favoriteRepository.GetFavoritesById(UserId, QuestionId);
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
