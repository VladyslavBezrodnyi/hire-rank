using AutoMapper;
using AutoMapper.QueryableExtensions;
using HireRank.Application.Services.Interfaces;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Campaigns
{
    public class GetAdminCampaignsQueryHandler : IRequestHandler<GetAdminCampaignsQuery, List<CampaignViewModel>>
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

        public async Task<List<CampaignViewModel>> Handle(GetAdminCampaignsQuery request, CancellationToken cancellationToken)
        {
            var adminId = _currentUserService.GetCurrentUserId();

            return await _store.Campaigns
                .AsNoTracking()
                .Where(x => x.AdminId == adminId)
                .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
                
        }
    }
}
