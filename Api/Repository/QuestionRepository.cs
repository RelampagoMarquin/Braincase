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

        async Task<IEnumerable<Question>>IQuestionRepository.GetAllQuestions()
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

    }
}