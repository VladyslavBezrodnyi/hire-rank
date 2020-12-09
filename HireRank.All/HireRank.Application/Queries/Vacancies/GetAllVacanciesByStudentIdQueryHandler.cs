using AutoMapper;
using HireRank.Application.ViewModels.Shared;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetAllVacanciesByStudentIdQueryHandler : IRequestHandler<GetAllVacanciesByStudentIdQuery, List<VacancyViewModel>>
    {
        private readonly IStore _store;
        private IMapper _mapper;

        public GetAllVacanciesByStudentIdQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<List<VacancyViewModel>> Handle(GetAllVacanciesByStudentIdQuery request, CancellationToken cancellationToken)
        {
            return await _store.StudentVacancies
                .Where(stv => stv.StudentId == request.StudentId)
                .Join(_store.Vacancies.Include(v => v.Campaign),
                stv => stv.VacancyId,
                v => v.Id,
                (stv, v) => new { Vacancy = v, Priority = stv.Priority })
                .OrderBy(obj => obj.Priority)
                .Select(obj => _mapper.Map<VacancyViewModel>(obj.Vacancy))
                .ToListAsync();
        }
    }
}
