using MediatR;
using System;

namespace HireRank.Application.Commands.Campaigns
{
    public class CreateCampaignCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
