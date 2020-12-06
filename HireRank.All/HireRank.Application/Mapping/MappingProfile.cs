﻿using AutoMapper;
using HireRank.Application.Commands.Account;
using HireRank.Application.Commands.Questions;
using HireRank.Application.Queries.Campaigns;
using HireRank.Application.ViewModels;
using HireRank.Application.ViewModels.Shared;
using HireRank.Core.Entities;

namespace HireRank.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapQueris();
            MapCommands();
        }

        private void MapQueris()
        {
            CreateMap<Campaign, CampaignViewModel>();
            CreateMap<Question, QuestionViewModel>();
            CreateMap<Option, OptionViewModel>();
            CreateMap<Student, StudentViewModel>();
            CreateMap<Employer, EmployerViewModel>();
            CreateMap<Vacancy, VacancyViewModel>();
            CreateMap<StudentVacancy, StudentVacancyViewModel>();
        }

        private void MapCommands()
        {

            CreateMap<CreateQuestionCommand, Question>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Options, opt => opt.Ignore())
                .ForMember(dest => dest.EmployerId, opt => opt.Ignore())
                .ForMember(dest => dest.Employer, opt => opt.Ignore())
                .ForMember(dest => dest.VacancyQuestions, opt => opt.Ignore());

            CreateMap<OptionViewModel, Option>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
                .ForMember(dest => dest.Question, opt => opt.Ignore());

            CreateMap<StudentRegisterCommand, Student>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<EmployerRegisterCommand, Employer>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
