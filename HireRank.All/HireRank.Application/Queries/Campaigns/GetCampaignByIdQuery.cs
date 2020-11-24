using MediatR;
using System;

namespace HireRank.Application.Queries.Campaigns
{
    public class GetCampaignByIdQuery : IRequest<CampaignViewModel>
    {
        public Guid Id { get; set; }

        public GetCampaignByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
