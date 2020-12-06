using HireRank.Application.ViewModels.Shared;
using MediatR;
using System;
using System.Collections.Generic;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetAllVacanciesByStudentIdQuery : IRequest<List<VacancyViewModel>>
    {
        public Guid StudentId { get; set; }

        public GetAllVacanciesByStudentIdQuery(Guid id)
        {
            StudentId = id;
        }
    }
}
