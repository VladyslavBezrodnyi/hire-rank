using MediatR;
using System;

namespace HireRank.Application.Commands.Campaigns
{
    public class DeleteCampaignCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
