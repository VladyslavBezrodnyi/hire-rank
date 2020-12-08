using AutoMapper;
using AutoMapper.QueryableExtensions;
using HireRank.Application.ViewModels;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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

        public GetActiveCampiagnsQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<List<ActiveCampiagnViewModel>> Handle(GetActiveCampiagnsQuery request, CancellationToken cancellationToken)
        {
            var activeCampaigns = await _store.Campaigns
                .ActiveCampaigns()
                .OrderBy(x => x.Name)
                .ProjectTo<ActiveCampiagnViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return activeCampaigns;
        }
    }
}
