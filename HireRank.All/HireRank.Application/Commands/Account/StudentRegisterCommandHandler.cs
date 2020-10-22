using AutoMapper;
using HireRank.Application.Services;
using HireRank.Application.ViewModels.Shared;
using HireRank.Common.Consts;
using HireRank.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Account
{
    public class StudentRegisterCommandHandler : IRequestHandler<StudentRegisterCommand, TokenResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public StudentRegisterCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Handle(StudentRegisterCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);

            await _userService.RegisterUserAsync(student, request.Password, Roles.Student);

            var userRole = await _userService.GetRolesAsync(student);

            return _userService.GenerateJwtToken(student, userRole);
        }
    }
}
