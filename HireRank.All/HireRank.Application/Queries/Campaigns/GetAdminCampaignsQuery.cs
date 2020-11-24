using MediatR;
using System.Collections.Generic;

namespace HireRank.Application.Queries.Campaigns
{
    public class GetAdminCampaignsQuery : IRequest<List<CampaignViewModel>>
    {
    }
}
