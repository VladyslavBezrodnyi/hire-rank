using AutoMapper;
using AutoMapper.QueryableExtensions;
using HireRank.Core.Extensions;
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

        public GetAllCampaignsQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<List<CampaignViewModel>> Handle(GetAllCampaignsQuery request, CancellationToken cancellationToken)
        {
            return await _store.Campaigns
                .AsNoTracking()
                .OrderByDescending(x => x.StartDate)
                //.ApplyPaging(request.PageSize, request.Page)
                .ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
