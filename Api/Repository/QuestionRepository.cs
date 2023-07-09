using System;
using Api.Data;
using Api.Models;
using Api.Repository;
using Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Api.Dto.Question;
using AutoMapper;

namespace Api.Repository
{
    public class QuestionRepository : BaseRepository , IQuestionRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;

        public QuestionRepository(APIDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<IEnumerable<Question>> IQuestionRepository.GetAllQuestions()
        {
            return await _context.Question.Include(x => x.Institution).ToListAsync();
        }

        async Task<Question?> IQuestionRepository.GetQuestionById(Guid id)
        {
            var question = await _context.Question.Include(x => x.Institution).FirstOrDefaultAsync(x => x.Id == id);
            if (question is null)
            {
                return null;
            }
            return question;
        }

        async Task<IEnumerable<Question>> IQuestionRepository.GetAllPublic()
        {
            return await _context.Question.Where(x => x.IsPrivate == false).Include(x => x.Institution)
                .ToListAsync();
        }

        async Task<IEnumerable<Question>> IQuestionRepository.GetMyQuestions(string id)
        {
            return await _context.Question.Include(x => x.Institution).Include(x => x.Favorites)
                .Where(x => x.Favorites.Any(x => x.UserId == id && x.Own == true)).ToListAsync();
        }

        async Task<IEnumerable<Question>> IQuestionRepository.GetMyFavorites(string id)
        {
            return await _context.Question.Include(x => x.Institution).Include(x => x.Favorites)
                .Where(x => x.Favorites.Any(x => x.UserId == id)).ToListAsync();
        }

        // este get pega todas as questões publicas e soma com as privadas do usuário
        async Task<IEnumerable<Question>> IQuestionRepository.GetAllUserQuestion(string id)
        {
            return await _context.Question.Include(x => x.Institution).Include(x => x.Favorites)
                .Where(x => x.Favorites.Any(x => x.UserId == id && x.Own == true) || x.IsPrivate == false).ToListAsync();
        }
    }
}