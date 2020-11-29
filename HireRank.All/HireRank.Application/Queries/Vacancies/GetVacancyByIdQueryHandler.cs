using AutoMapper;
using HireRank.Application.ViewModels.Shared;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetVacancyByIdQueryHandler : IRequestHandler<GetVacancyByIdQuery, VacancyViewModel>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;

        public GetVacancyByIdQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<VacancyViewModel> Handle(GetVacancyByIdQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _store.Vacancies.AsNoTracking().Include(x => x.Campaign).WithIdAsync(request.Id);

            var vacancyViewModel = _mapper.Map<VacancyViewModel>(vacancy);

            //vacancyViewModel.Campaign = _mapper.Map<CampaignViewModel>(vacancy.Campaign);

            return vacancyViewModel;
        }
    }
}
