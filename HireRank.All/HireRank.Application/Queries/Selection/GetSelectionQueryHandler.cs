using AutoMapper;
using HireRank.Application.ViewModels;
using HireRank.Core.Store;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace HireRank.Application.Queries.Selection
{
    public class GetSelectionQueryHandler : IRequestHandler<GetSelectionQuery, List<StudentVacancyViewModel>>
    {
        private readonly IStore _store;
        private IMapper _mapper;

        public GetSelectionQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<List<StudentVacancyViewModel>> Handle(GetSelectionQuery request, CancellationToken cancellationToken)
        {
            return await _store.StudentVacancies
                .Where(s => s.VacancyId == request.Id && s.IsMatch == true)
                .ProjectTo<StudentVacancyViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
