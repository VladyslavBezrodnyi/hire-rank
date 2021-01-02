using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HireRank.Application.Commands.Admin;
using HireRank.Application.Queries.Admin;
using HireRank.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HireRank.WebApi.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("allEmployer")]
        public async Task<List<EmployerViewModel>> GetEmployersForConfirmation()
            => await _mediator.Send(new GetEmployersForConfirmationQuery());

        [Authorize(Roles = "admin")]
        [HttpGet("nonConfirmedEmployer")]
        public async Task<List<EmployerViewModel>> GetNonConfirmedEmployers() 
            => await _mediator.Send(new GetNonConfirmedEmployersQuery());


        [Authorize(Roles = "admin")]
        [HttpPost("confirm")]
        public async Task<bool> ConfirmEmployer([FromBody] ConfirmEmployerCommand request)
            => await _mediator.Send(request);
    }
}
