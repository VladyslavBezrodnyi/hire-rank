using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HireRank.Application.Commands.Vacancies;
using HireRank.Application.Filtering;
using HireRank.Application.Queries.Vacancies;
using HireRank.Application.ViewModels.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireRank.WebApi.Controllers
{
    [Route("api/vacancies")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VacanciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PagedResult<VacancyViewModel>> GetAllVacanciesAsync([FromQuery]GetVacanciesQuery query)
            => await _mediator.Send(query);

        [Authorize(Roles ="employer")]
        [HttpGet("employer")]
        public async Task<PagedResult<VacancyViewModel>> GetEmployerVacanciesAsync([FromQuery]GetEmployerVacanciesQuery query)
            => await _mediator.Send(query);

        [Authorize(Roles = "employer")]
        [HttpGet("{id}")]
        public async Task<VacancyViewModel> GetVacancyAsync(Guid id)
            => await _mediator.Send(new GetVacancyByIdQuery() { Id = id});

        [Authorize(Roles = "employer")]
        [HttpPost]
        public async Task<Guid> CreateVacancyAsync([FromBody]CreateVacancyCommand request)
            => await _mediator.Send(request);

        [Authorize(Roles = "employer")]
        [HttpPut]
        public async Task<Guid> UpdateVacancyAsync([FromBody]UpdateVacancyCommand request)
            => await _mediator.Send(request);

        [Authorize(Roles = "employer")]
        [HttpDelete("{id}")]
        public async Task<Guid> UpdateVacancyAsync(Guid id)
            => await _mediator.Send(new DeleteVacancyCommand() { Id = id });

        [Authorize(Roles = "student")]
        [HttpGet("student/{studentId}")]
        public async Task<List<VacancyViewModel>> GetAllStudentVacanciesAsync(Guid studentId) 
            => await _mediator.Send(new GetAllVacanciesByStudentIdQuery(studentId));

        [Authorize(Roles = "student")]
        [HttpPost("assign-priority")]
        public async Task<Guid> Post([FromBody] AssignVacancyPriorityCommand command)
            => await _mediator.Send(command);
    }
}