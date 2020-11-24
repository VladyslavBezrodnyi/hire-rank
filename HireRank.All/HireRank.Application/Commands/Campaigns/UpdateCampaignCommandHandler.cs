using HireRank.Common.Exceptions;
using HireRank.Core.Entities;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Campaigns
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, Guid>
    {
        private readonly IStore _store;

        public UpdateCampaignCommandHandler(IStore store)
        {
            _store = store;
        }

        public async Task<Guid> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _store.Campaigns.WithIdAsync(request.Id);

            campaign.Name = request.Name;
            campaign.StartDate = request.StartDate;
            campaign.EndDate = request.EndDate;

            await _store.SaveChangesAsync();

            return request.Id;
        }
    }
}
