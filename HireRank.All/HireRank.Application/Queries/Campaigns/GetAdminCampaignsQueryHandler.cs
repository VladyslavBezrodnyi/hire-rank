using AutoMapper;
using HireRank.Application.Filtering;
using HireRank.Application.Services.Interfaces;
using HireRank.Core.Entities;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Campaigns
{
    public class GetAdminCampaignsQueryHandler : IRequestHandler<GetAdminCampaignsQuery, PagedResult<CampaignViewModel>>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetAdminCampaignsQueryHandler(IStore store, IMapper mapper, ICurrentUserService currentUserService)
        {
            _store = store;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<CampaignViewModel>> Handle(GetAdminCampaignsQuery request, CancellationToken cancellationToken)
        {
            var adminId = _currentUserService.GetCurrentUserId();

            return await _store.Campaigns
                .AsNoTracking()
                .Where(x => x.AdminId == adminId)
                .ApplyQueryAsync<Campaign, CampaignViewModel>(request, _mapper.ConfigurationProvider);
                
        }
    }
}
