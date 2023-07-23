using System;
using Api.Data;
using Api.Models;
using Api.Repository;
using Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Api.Dto.Question;
using AutoMapper;
using System.Transactions;

namespace Api.Repository
{
    public class QuestionRepository : BaseRepository, IQuestionRepository
    {
        private readonly APIDbContext _context;
        private readonly IMapper _mapper;

        public QuestionRepository(APIDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        // pega todas as questões do banco
        async Task<IEnumerable<Question>> IQuestionRepository.GetAllQuestions()
        {
            return await _context.Question.Include(x => x.Institution).ToListAsync();
        }

        async Task<Question?> IQuestionRepository.GetQuestionById(Guid id)
        {
            var question = await _context.Question.Include(x => x.Institution)
                    .Include(x => x.Tags).ThenInclude(tag => tag.Subject)
                    .FirstOrDefaultAsync(x => x.Id == id);
            if (question is null)
            {
                return null;
            }
            return question;
        }
        // pega todas as publicas 
        async Task<IEnumerable<Question>> IQuestionRepository.GetAllPublic()
        {
            return await _context.Question.Where(x => x.IsPrivate == false)
                .Include(x => x.Institution)
                .Include(x => x.Favorites)
                .Include(x => x.Tags).ThenInclude(tag => tag.Subject)
                .ToListAsync();
        }

        // pega somente as questões que o usuario criou
        async Task<IEnumerable<Question>> IQuestionRepository.GetMyQuestions(string id)
        {
            return await _context.Question.Include(x => x.Institution)
                .Include(x => x.Favorites).Where(x => x.Favorites.Any(x => x.UserId == id && x.Own == true))
                .Include(x => x.Tags).ThenInclude(tag => tag.Subject)
                .ToListAsync();
        }

        // pega todos as questões favoritas
        async Task<IEnumerable<Question>> IQuestionRepository.GetMyFavorites(string id)
        {
            return await _context.Question.Include(x => x.Institution)
                .Include(x => x.Favorites).Where(x => x.Favorites.Any(favorite => favorite.UserId == id))
                .Include(x => x.Tags).ThenInclude(tag => tag.Subject)
                .ToListAsync();
        }

        // este get pega todas as questões publicas e soma com as privadas do usuário
        async Task<IEnumerable<Question>> IQuestionRepository.GetAllUserQuestion(string id)
        {
            return await _context.Question.Include(x => x.Institution).Include(x => x.Favorites)
                .Where(x => x.Favorites.Any(x => x.UserId == id && x.Own == true) || x.IsPrivate == false)
                .Include(x => x.Tags).ThenInclude(tag => tag.Subject)
                .ToListAsync();
        }

        public async Task<Question> CreateQuestion(CreateQuestionDTO createQuestionDTO, string userId)
        {
            // verify tag if not exist create
            var tags = new List<Tag>();
            foreach (var tag in createQuestionDTO.Tags)
            {
                var tagAux = await _context.Tag.FirstOrDefaultAsync(x => x.Name == tag);
                if (tagAux == null)
                {
                    var newTag = new Tag
                    {
                        Name = tag,
                        SubjectId = createQuestionDTO.SubjectId,
                    };
                    await _context.AddAsync(newTag);
                    await _context.SaveChangesAsync();
                    tagAux = await _context.Tag.FirstOrDefaultAsync(x => x.Name == tag);
                    if (tagAux != null)
                    {
                        tags.Add(tagAux);
                    }
                }
                else
                {
                    tags.Add(tagAux);
                }
            }

            // Verify Institution if not exist create
            var institution = await _context.Institution
                            .FirstOrDefaultAsync(x => x.Name == createQuestionDTO.InstitutionName);
            if (!String.IsNullOrEmpty(createQuestionDTO.InstitutionName))
            {
                if (institution == null)
                {
                    var newInstitution = new Institution { Name = createQuestionDTO.InstitutionName };
                    await _context.AddAsync(newInstitution);
                    await _context.SaveChangesAsync();
                    institution = await _context.Institution
                        .FirstOrDefaultAsync(x => x.Name == createQuestionDTO.InstitutionName);
                }
            }
            
            // Create Question
            Question question = new Question
            {
                Dificult = createQuestionDTO.Dificult,
                IsPrivate = createQuestionDTO.IsPrivate,
                Text = createQuestionDTO.Text,
                InstitutionId = institution?.Id,
                Justify = createQuestionDTO.Justify,
                Type = createQuestionDTO.Type,
            };

            foreach (var tag in tags)
            {
                question.Tags.Add(tag);
            }

            await _context.AddAsync(question);
            await _context.SaveChangesAsync();

            var questionAux = await _context.Question.Where(x =>
                    x.Text == createQuestionDTO.Text &&
                    x.Justify == createQuestionDTO.Justify &&
                    x.IsPrivate == createQuestionDTO.IsPrivate &&
                    x.Dificult == createQuestionDTO.Dificult &&
                    x.Type == createQuestionDTO.Type
                ).OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();


            if (questionAux != null)
            {
                question = questionAux;
            }

            // Create Answer
            foreach (var answer in createQuestionDTO.Answers)
            {
                var answerAux = new Answer
                {
                    QuestionId = question.Id,
                    Text = answer.Text,
                    IsCorrect = answer.IsCorrect,
                };
                await _context.AddAsync(answerAux);
                await _context.SaveChangesAsync();
            }

            // Add to favorites
            var favorites = new Favorites
            {
                UserId = userId,
                QuestionId = question.Id,
                Own = true
            };
            await _context.AddAsync(favorites);
            await _context.SaveChangesAsync();

            return question;
        }

    }
}