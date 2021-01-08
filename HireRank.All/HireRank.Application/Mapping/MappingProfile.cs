using AutoMapper;
using HireRank.Application.Commands.Account;
using HireRank.Application.Commands.Questions;
using HireRank.Application.Commands.Vacancies;
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
            CreateMap<Campaign, CampaignViewModel>()
                 .ForMember(dest => dest.State, opt => opt.Ignore());
            CreateMap<Campaign, ActiveCampiagnViewModel>();
            CreateMap<Question, QuestionViewModel>();
            CreateMap<Question, AvailableVacancyQuestionViewModel>()
                 .ForMember(dest => dest.Selected, opt => opt.Ignore());
            CreateMap<Option, OptionViewModel>();
            CreateMap<Student, StudentViewModel>();
            CreateMap<Employer, EmployerViewModel>();
            CreateMap<Vacancy, VacancyViewModel>();
            CreateMap<Question, TestQuestionViewModel>();
            CreateMap<Option, TestOptionViewModel>();
            CreateMap<StudentVacancy, StudentVacancyViewModel>()
                .ForMember(dest => dest.Order, opt => opt.Ignore());
        }

        private void MapCommands()
        {
            CreateMap<CreateQuestionCommand, Question>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Options, opt => opt.Ignore())
                .ForMember(dest => dest.EmployerId, opt => opt.Ignore())
                .ForMember(dest => dest.Employer, opt => opt.Ignore())
                .ForMember(dest => dest.VacancyQuestions, opt => opt.Ignore());

            CreateMap<CreateVacancyCommand, Vacancy>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Employer, opt => opt.Ignore())
                .ForMember(dest => dest.EmployerId, opt => opt.Ignore())
                .ForMember(dest => dest.Campaign, opt => opt.Ignore())
                .ForMember(dest => dest.StudentVacancies, opt => opt.Ignore())
                .ForMember(dest => dest.VacancyQuestions, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore());

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
