using AutoMapper;
using HireRank.Application.Filtering;
using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels.Shared;
using HireRank.Core.Entities;
using HireRank.Core.Extensions;
using HireRank.Core.StablePairing;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetVacanciesQueryHandler : IRequestHandler<GetVacanciesQuery, PagedResult<VacancyViewModel>>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICampaignProcessingState _campaignProcessingState;

        public GetVacanciesQueryHandler(IStore store, IMapper mapper, ICurrentUserService currentUserService, ICampaignProcessingState campaignProcessingState)
        {
            _store = store;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _campaignProcessingState = campaignProcessingState;
        }

        public async Task<PagedResult<VacancyViewModel>> Handle(GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            var vacancies = await _store.Vacancies
                .Include(x => x.Campaign)
                .Include(x => x.Employer)
                .Active()
                .WithTestBase()
                .ApplyQueryAsync<Vacancy, VacancyViewModel>(request, _mapper.ConfigurationProvider);

            return vacancies;
        }
    }
}
