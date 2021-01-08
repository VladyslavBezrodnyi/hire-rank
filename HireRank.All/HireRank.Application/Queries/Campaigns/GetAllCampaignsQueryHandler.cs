using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class GetAllCampaignsQueryHandler : IRequestHandler<GetAllCampaignsQuery, List<CampaignViewModel>>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;
        private readonly ICampaignProcessingState _campaignProcessingState;

        public GetAllCampaignsQueryHandler(IStore store, IMapper mapper, ICampaignProcessingState campaignProcessingState)
        {
            _store = store;
            _mapper = mapper;
            _campaignProcessingState = campaignProcessingState;
        }

        public async Task<List<CampaignViewModel>> Handle(GetAllCampaignsQuery request, CancellationToken cancellationToken)
        {
            var campaigns =  await _store.Campaigns
                .AsNoTracking()
                .OrderByDescending(x => x.StartDate)
                //.ApplyPaging(request.PageSize, request.Page)
                .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            foreach(var campaign in campaigns)
            {
                campaign.State = await _campaignProcessingState.CheckStateOfProcessingAsync(campaign.Id);
            }

            return campaigns;
        }
    }
}
