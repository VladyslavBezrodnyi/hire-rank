using AutoMapper;
using HireRank.Application.Commands.Account;
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
            // mapping for queries
        }

        private void MapCommands()
        {
            CreateMap<StudentRegisterCommand, Student>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
