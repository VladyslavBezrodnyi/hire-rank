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
using System;

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
            var vacancy = await _store.Vacancies
                .Include(v => v.Campaign)
                .FirstOrDefaultAsync(v => v.Id == request.Id);

            return await _store.StudentVacancies
                .Where(s => s.VacancyId == request.Id)
                .Where(s => vacancy.Campaign.EndDate < DateTime.Now ? s.IsMatch == true : true)
                .ProjectTo<StudentVacancyViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
