using MediatR;
using System.Collections.Generic;

namespace HireRank.Application.Queries.Campaigns
{
    public class GetAllCampaignsQuery : IRequest<List<CampaignViewModel>>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
