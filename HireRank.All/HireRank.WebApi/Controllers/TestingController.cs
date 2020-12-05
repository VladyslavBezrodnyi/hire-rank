using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HireRank.Application.Commands.Testing;
using HireRank.Application.Queries.Testing;
using HireRank.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireRank.WebApi.Controllers
{
    [Route("api/testing")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{vacancyId}")]
        [Authorize(Roles = "student")]
        public async Task<TestViewModel> GetTestByVacancyId(Guid vacancyId)
            => await _mediator.Send(new GetTestByVacancyIdQuery(vacancyId));

        [HttpPost("addTestResult")]
        [Authorize(Roles = "student")]
        public async Task<short> CreateQuestionAsync([FromBody] AddTestResultCommand request)
            => await _mediator.Send(request);


    }
}
