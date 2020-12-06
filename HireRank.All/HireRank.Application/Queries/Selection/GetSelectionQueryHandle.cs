using AutoMapper;
using HireRank.Application.ViewModels;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HireRank.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HireRank.Application.Queries.Selection
{
    public class GetSelectionQueryHandle : IRequestHandler<GetSelectionQuery, List<StudentVacancyViewModel>>
    {
        private readonly IStore _store;
        private IMapper _mapper;

        public GetSelectionQueryHandle(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<List<StudentVacancyViewModel>> Handle(GetSelectionQuery request, CancellationToken cancellationToken)
        {
            List<StudentVacancy> students = await _store.StudentVacancies.Where(s => s.VacancyId == request.Id && s.IsMatch == true).ToListAsync();
            List<StudentVacancyViewModel> studentsViewModel = _mapper.Map<List<StudentVacancyViewModel>>(students);
            return studentsViewModel;
        }
    }
}
