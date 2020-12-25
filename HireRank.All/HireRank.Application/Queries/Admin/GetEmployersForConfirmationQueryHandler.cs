using AutoMapper;
using AutoMapper.QueryableExtensions;
using HireRank.Application.ViewModels;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Admin
{
    public class GetEmployersForConfirmationQueryHandler : IRequestHandler<GetEmployersForConfirmationQuery, List<EmployerViewModel>>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;

        public GetEmployersForConfirmationQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<List<EmployerViewModel>> Handle(GetEmployersForConfirmationQuery request, CancellationToken cancellationToken)
        {
            var employers = await _store.Employers
                .OrderBy(x => x.IsConfirmed)
                .ThenBy(x => x.CompanyName)
                .ProjectTo<EmployerViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return employers;
        }
    }
}
