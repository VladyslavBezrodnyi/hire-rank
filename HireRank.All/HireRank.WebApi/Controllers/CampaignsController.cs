using HireRank.Application.Commands.Campaigns;
using HireRank.Application.Filtering;
using HireRank.Application.Queries.Campaigns;
using HireRank.Application.ViewModels;
using HireRank.Core.StablePairing;
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
        private readonly ICampaignProcessingState _campaignProcessingState;

        public CampaignsController(IMediator mediator, ICampaignProcessingState campaignProcessingState)
        {
            _mediator = mediator;
            _campaignProcessingState = campaignProcessingState;
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

        [HttpGet("active")]
        public async Task<List<ActiveCampiagnViewModel>> GetActiveCampaignsAsync()
        {
            return await _mediator.Send(new GetActiveCampiagnsQuery());
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("{id}/close")]
        public async Task CloseCampaignAsync(Guid id)
        {
            await _mediator.Send(new CloseCampaignCommand() { CampaignId = id });
        }

        [HttpGet("{id}/state")]
        public async Task<CampaignProcessingStates> CheckStateOfCampaignProcessing(Guid id)
            => await _campaignProcessingState.CheckStateOfProcessingAsync(id);
    }
}
