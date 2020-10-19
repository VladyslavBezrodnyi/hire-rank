using HireRank.Application.ViewModels.Shared;
using MediatR;

namespace HireRank.Application.Commands.Account
{
    public class LoginCommand : IRequest<TokenResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
