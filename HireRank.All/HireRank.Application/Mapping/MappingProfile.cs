using AutoMapper;
using HireRank.Application.Commands.Account;
using HireRank.Application.Queries.Campaigns;
using HireRank.Application.ViewModels;
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
            CreateMap<Student, StudentViewModel>();
            CreateMap<Employer, EmployerViewModel>();
        }

        private void MapCommands()
        {
            CreateMap<StudentRegisterCommand, Student>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<EmployerRegisterCommand, Employer>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
