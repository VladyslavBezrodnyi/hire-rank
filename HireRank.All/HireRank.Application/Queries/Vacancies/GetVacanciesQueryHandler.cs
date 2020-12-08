using AutoMapper;
using HireRank.Application.Filtering;
using HireRank.Application.ViewModels.Shared;
using HireRank.Core.Entities;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, PagedResult<VacancyViewModel>>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;

        public GetVacanciesQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<PagedResult<VacancyViewModel>> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            var vacancies = await _store.Vacancies
                .Include(x => x.Campaign)
                .Include(x => x.Employer)
                .ActiveVacancies()
                .ApplyQueryAsync<Vacancy, VacancyViewModel>(request, _mapper.ConfigurationProvider);

            return vacancies;
        }
    }
}
