using AutoMapper;
using AutoMapper.QueryableExtensions;
using HireRank.Application.ViewModels;
using HireRank.Core.Extensions;
using HireRank.Core.StablePairing;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Campaigns
{
    public class GetActiveCampiagnsQueryHandler : IRequestHandler<GetActiveCampiagnsQuery, List<ActiveCampiagnViewModel>>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;
        private readonly ICampaignProcessingState _campaignProcessingState;

        public GetActiveCampiagnsQueryHandler(IStore store, IMapper mapper, ICampaignProcessingState campaignProcessingState)
        {
            _store = store;
            _mapper = mapper;
            _campaignProcessingState = campaignProcessingState;
        }

        public async Task<List<ActiveCampiagnViewModel>> Handle(GetActiveCampiagnsQuery request, CancellationToken cancellationToken)
        {
            var activeCampaigns = await _store.Campaigns
                .Active()
                .OrderBy(x => x.Name)
                .ProjectTo<ActiveCampiagnViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var campaigns = new List<ActiveCampiagnViewModel>();
            foreach(var c in activeCampaigns)
            {
                if (await _campaignProcessingState.CheckStateOfProcessingAsync(c.Id) != CampaignProcessingStates.Finished)
                {
                    campaigns.Add(c);
                }
            }

            return campaigns;
        }
    }
}
