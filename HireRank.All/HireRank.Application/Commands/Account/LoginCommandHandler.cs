using HireRank.Application.Services;
using HireRank.Application.ViewModels.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Account
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
    {
        private readonly IUserService _userService;

        public LoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var foundUser = await _userService.FindUserByEmail(request.Email);
            await _userService.CheckIfThePasswordIsCorrect(foundUser, request.Password);
            var userRole = await _userService.GetRolesAsync(foundUser);

            return _userService.GenerateJwtToken(foundUser, userRole);
        }
    }
}
