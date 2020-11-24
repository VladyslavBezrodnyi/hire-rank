using AutoMapper;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Campaigns
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, CampaignViewModel>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;

        public GetCampaignByIdQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<CampaignViewModel> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var campaign = await _store.Campaigns.WithIdAsync(request.Id);

            var campaignViewModel = _mapper.Map<CampaignViewModel>(campaign);

            return campaignViewModel;
        }
    }
}
