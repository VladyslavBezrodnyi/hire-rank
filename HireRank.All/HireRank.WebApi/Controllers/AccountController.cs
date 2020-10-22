using HireRank.Application.Commands.Account;
using HireRank.Application.ViewModels.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HireRank.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<TokenResponse> LoginAsync([FromBody] LoginCommand request)
            => await _mediator.Send(request);

        [HttpPost("register/student")]
        public async Task<TokenResponse> RegisterStudentAsync([FromBody] StudentRegisterCommand request)
            => await _mediator.Send(request);
    }
}