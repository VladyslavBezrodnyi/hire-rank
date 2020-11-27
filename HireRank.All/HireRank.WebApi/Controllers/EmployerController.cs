using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HireRank.Application.Commands.Employers;
using HireRank.Application.Queries.Employer;
using HireRank.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireRank.WebApi.Controllers
{
    [Route("api/employer")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "employer")]
        [HttpGet("profile")]
        public async Task<EmployerViewModel> GetEmployerAsync()
        {
            GetEmployerByIdQuery request = new GetEmployerByIdQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return await _mediator.Send(request);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<EmployerViewModel> GetEmployerAsync(Guid id)
        {
            GetEmployerByIdQuery request = new GetEmployerByIdQuery(id);
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "employer")]
        [HttpPut("update")]
        public async Task<EmployerViewModel> UpdateStudentAsync([FromBody] UpdateEmployerCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
