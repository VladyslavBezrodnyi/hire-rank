using MediatR;
using System;

namespace HireRank.Application.Commands.Campaigns
{
    public class UpdateCampaignCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
