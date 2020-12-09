using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HireRank.Application.Commands.Students;
using HireRank.Application.Queries.Student;
using HireRank.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireRank.WebApi.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "student")]
        [HttpGet("profile")]
        public async Task<StudentViewModel> GetStudentAsync()
        {
            GetStudentByIdQuery request = new GetStudentByIdQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return await _mediator.Send(request);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<StudentViewModel> GetStudentAsync(Guid id)
        {
            GetStudentByIdQuery request = new GetStudentByIdQuery(id);
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "student")]
        [HttpPut("update")]
        public async Task<StudentViewModel> UpdateStudentAsync([FromBody] UpdateStudentCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
