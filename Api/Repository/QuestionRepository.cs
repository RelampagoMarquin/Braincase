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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            var question = await _context.Question
                .Include(x => x.Institution)
                .Include(x => x.Answers)
                .Include(x => x.Favorites).ThenInclude(favorite => favorite.User).Where(x => x.Favorites.Any(x => x.Own == true))
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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // verify tag if not exist create
                    var tags = new List<Tag>();
                    if (! (createQuestionDTO.Tags.Count() > 0)) {
                        throw new Exception("Não há tags");
                    }
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
                            tags.Add(newTag);
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

                    // Commit transaction if all commands succeed
                    transaction.Commit();

                    return question;
                }
                catch (Exception)
                {
                    // Rollback transaction if something goes wrong
                    transaction.Rollback();

                    throw;
                }
            }
        }

        public async Task<Question> UpdateQuestion(UpdateQuestionDTO updateQuestionDTO, Question question)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // verify tags
                    var tagsAux = new List<Tag>();
                    if (!(updateQuestionDTO.Tags.Count() > 0))
                    {
                        throw new Exception("Não há tags");
                    }
                    foreach (var tag in updateQuestionDTO.Tags)
                    {
                        var tagAux = await _context.Tag.FirstOrDefaultAsync(x => x.Name == tag);
                        if (tagAux == null)
                        {
                            var newTag = new Tag
                            {
                                Name = tag,
                                SubjectId = updateQuestionDTO.SubjectId,
                            };
                            await _context.AddAsync(newTag);
                            tagsAux.Add(newTag);
                        }
                        else
                        {
                            tagsAux.Add(tagAux);
                        }
                    }
                    question.Tags.Clear();
                    foreach (var tag in tagsAux)
                    {
                        question.Tags.Add(tag);
                    }

                    // Verify Institution if not exist create
                    var institution = question.Institution;
                    if(updateQuestionDTO.InstitutionName != question.Institution?.Name)
                    {
                        institution = await _context.Institution
                                    .FirstOrDefaultAsync(x => x.Name == updateQuestionDTO.InstitutionName);
                        if (!String.IsNullOrEmpty(updateQuestionDTO.InstitutionName))
                        {
                            if (institution == null)
                            {
                            var newInstitution = new Institution { Name = updateQuestionDTO.InstitutionName };
                            await _context.AddAsync(newInstitution);
                            }
                        }
                    }

                    // Verify Answers
                    var answersAux = new List<Answer>();
                    foreach (var answer in updateQuestionDTO.Answers)
                    {
                        var answerAux = await _context.Answer
                                        .FirstOrDefaultAsync(x => x.QuestionId == question.Id && x.Text == answer.Text);
                        if(answerAux == null) 
                        { 
                            answerAux = new Answer
                            {
                                QuestionId = question.Id,
                                Text = answer.Text,
                                IsCorrect = answer.IsCorrect,
                            };
                            await _context.AddAsync(answerAux);
                        }
                        answersAux.Add(answerAux);
                    }
                    foreach (var res in question.Answers.ToList())
                    {
                        var resAux = answersAux.FirstOrDefault(aux => aux.Id == res.Id);
                        if (resAux == null)
                        {
                            _context.Remove(res);
                        }
                    }

                    // Update Question
                    question.Dificult = updateQuestionDTO.Dificult;
                    question.IsPrivate = updateQuestionDTO.IsPrivate;
                    question.Text = updateQuestionDTO.Text;
                    question.InstitutionId = institution?.Id;
                    question.Justify = updateQuestionDTO.Justify;
                    question.Type = updateQuestionDTO.Type;

                    // Execute update
                    _context.Update(question);
                    await _context.SaveChangesAsync();

                    // finish transaction
                    transaction.Commit();
                    return question;
                }
                catch (Exception)
                {
                    // Rollback transaction if something goes wrong
                    transaction.Rollback();

                    throw;
                }
        }
    }
    }
}