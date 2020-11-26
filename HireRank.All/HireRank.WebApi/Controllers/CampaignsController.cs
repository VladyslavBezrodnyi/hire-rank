using HireRank.Application.Commands.Campaigns;
using HireRank.Application.Filtering;
using HireRank.Application.Queries.Campaigns;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HireRank.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<Guid> CreateCampaignAsync([FromBody]CreateCampaignCommand command)
            => await _mediator.Send(command);

        [Authorize(Roles = "admin")]
        [HttpPatch]
        public async Task<Guid> UpdateCampaignAsync([FromBody]UpdateCampaignCommand command)
            => await _mediator.Send(command);

        [Authorize(Roles ="admin")]
        [HttpDelete("{id}")]
        public async Task<Guid> UpdateCampaignAsync(Guid id)
            => await _mediator.Send(new DeleteCampaignCommand() { Id = id });

        [Authorize]
        [HttpGet]
        public async Task<List<CampaignViewModel>> GetAllCampaignsAsync()
        {
            var request = new GetAllCampaignsQuery();
            return await _mediator.Send(request);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<CampaignViewModel> GetCampaignByIdAsync(Guid id)
        {
            var request = new GetCampaignByIdQuery(id);
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public async Task<PagedResult<CampaignViewModel>> GetAdminCampaignsAsync([FromQuery]GetAdminCampaignsQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
