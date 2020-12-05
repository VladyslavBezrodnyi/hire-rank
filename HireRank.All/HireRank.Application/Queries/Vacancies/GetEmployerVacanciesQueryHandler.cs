using AutoMapper;
using HireRank.Application.Filtering;
using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels.Shared;
using HireRank.Core.Entities;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetEmployerVacanciesQueryHandler : IRequestHandler<GetEmployerVacanciesQuery, PagedResult<VacancyViewModel>>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetEmployerVacanciesQueryHandler(IStore store, IMapper mapper, ICurrentUserService currentUserService)
        {
            _store = store;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<VacancyViewModel>> Handle(GetEmployerVacanciesQuery request, CancellationToken cancellationToken)
        {
            var employerId = _currentUserService.GetCurrentUserId();

            var vacancies = await _store.Vacancies
                .Include(x => x.Campaign)
                .Include(x => x.Employer)
                .Where(x => x.EmployerId == employerId)
                .ApplyQueryAsync<Vacancy, VacancyViewModel>(request, _mapper.ConfigurationProvider);

            return vacancies;
        }
    }
}
