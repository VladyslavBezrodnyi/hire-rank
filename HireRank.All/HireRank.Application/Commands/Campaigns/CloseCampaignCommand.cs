using MediatR;
using System;

namespace HireRank.Application.Commands.Campaigns
{
    public class CloseCampaignCommand : IRequest<Guid>
    {
        public Guid CampaignId { get; set; }
    }
}
