using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HireRank.Application.Commands.Questions;
using HireRank.Application.Filtering;
using HireRank.Application.Queries.Questions;
using HireRank.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireRank.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("tags")]
        [Authorize(Roles = "employer")]
        public async Task<List<string>> GetQuestionTagsAsync()
            => await _mediator.Send(new GetQuestionTagsQuery());

        [HttpGet]
        [Authorize(Roles = "employer")]
        public async Task<PagedResult<QuestionViewModel>> GetEmployerQuestionsAsync([FromQuery]GetEmployerQuestionQuery query)
            => await _mediator.Send(query);

        [HttpGet("{id}")]
        [Authorize]
        public async Task<QuestionWithOptionsViewModel> GetQuestionAsync(Guid id)
            => await _mediator.Send(new GetQuestionByIdQuery() { Id = id });

        [HttpPost]
        [Authorize(Roles = "employer")]
        public async Task<Guid> CreateQuestionAsync([FromBody]CreateQuestionCommand request)
            => await _mediator.Send(request);

        [HttpPut]
        [Authorize(Roles = "employer")]
        public async Task<Guid> EditQuestionAsync([FromBody]EditQuestionCommand request)
            => await _mediator.Send(request);


        [HttpDelete("{id}")]
        [Authorize(Roles = "employer")]
        public async Task<Guid> DeleteQuestionAsync(Guid id)
            => await _mediator.Send(new DeleteQuestionCommand() { Id = id });

    }
}