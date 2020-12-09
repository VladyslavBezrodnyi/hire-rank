using HireRank.Application.ViewModels.Shared;
using MediatR;
using System;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetVacancyByIdQuery : IRequest<VacancyViewModel>
    {
        public Guid Id { get; set; }
    }
}
