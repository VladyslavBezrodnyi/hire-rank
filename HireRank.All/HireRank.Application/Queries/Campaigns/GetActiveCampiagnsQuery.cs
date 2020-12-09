using HireRank.Application.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace HireRank.Application.Queries.Campaigns
{
    public class GetActiveCampiagnsQuery : IRequest<List<ActiveCampiagnViewModel>>
    {
    }
}
