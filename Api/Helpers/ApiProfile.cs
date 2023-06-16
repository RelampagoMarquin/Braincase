using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto.Institution;
using Api.Dto.Subject;
using Api.Dto.Tag;
using Api.Dto.Test;
using Api.Dto.User;
using Api.Dto.Question;
using Api.Dto.Comment;
using Api.Dto.Answer;
using Api.Dto.Favorites;
using Api.Models;
using AutoMapper;

namespace Api.Helpers
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<byte?, byte>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<Guid?, Guid>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<Boolean?, Boolean>().ConvertUsing((src, dest) => src ?? dest);
            // talvez essa conversão não funcione, tem que testar
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

            // mapper das instituições
            CreateMap<Institution, ResponseInstitutionDTO>();
            CreateMap<InstitutionDTO, Institution>();

            // mapper das tags
            CreateMap<Tag, ResponseTagDTO>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));
            CreateMap<UpdateTagDTO, Tag>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        if (srcMember == null)
                            return false;
                        
                        return true;
                    });
                });
            CreateMap<CreateTagDTO, Tag>();

            // mapper de subject
            CreateMap<Subject, ResponseSubjectDTO>();
            CreateMap<SubjectDTO, Subject>();

            // mapper de user
            CreateMap<User, ResposeUserDTO>();
            CreateMap<UpdateUserDTO, User>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        if (srcMember == null)
                            return false;

                        return true;
                    });
                });
            CreateMap<CreateUserDTO, User>();

            // mapper de test
            CreateMap<Test, ResponseTestDTO>();
            CreateMap<UpdateTestDTO, Test>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        if (srcMember == null)
                            return false;

                        return true;
                    });
                });
            CreateMap<CreateTestDTO, Test>();

            // mapper de user

            // mapper de question
            CreateMap<Question, ResponseQuestionDTO>()
                .ForMember(dest => dest.InstitutionName, opt => opt.MapFrom(src => src.Institution.Name));
            CreateMap<UpdateQuestionDTO, Question>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        if (srcMember == null)
                            return false;

                        return true;
                    });
                });
            CreateMap<CreateQuestionDTO, Question>();

            // mapper de comment
            CreateMap<Comment, ResponseCommentDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<CommentDTO, Comment>();
            CreateMap<CreateCommentDTO, Comment>();

            // mapper de answer
            CreateMap<Answer, ResponseAnswerDTO>();
            CreateMap<UpdateAnswerDTO, Answer>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) =>
                    {
                        if (srcMember == null)
                            return false;

                        return true;
                    });
                });
            CreateMap<CreateAnswerDTO, Answer>();

            // mapper de favorites
            CreateMap<Favorites, ResponseFavoritesDTO>();
            CreateMap<FavoritesDTO, Favorites>();
            
            
        }
    }
}