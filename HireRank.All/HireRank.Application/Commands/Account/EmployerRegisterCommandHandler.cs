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
    public class EmployerRegisterCommandHandler: IRequestHandler<EmployerRegisterCommand, TokenResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EmployerRegisterCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Handle(EmployerRegisterCommand request, CancellationToken cancellationToken)
        {
            var employer = _mapper.Map<Employer>(request);

            await _userService.RegisterUserAsync(employer, request.Password, Roles.Employer);

            var userRole = await _userService.GetRolesAsync(employer);

            return _userService.GenerateJwtToken(employer, userRole);
        }
    }
}
