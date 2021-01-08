using HireRank.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetAvailableQuestionsQuery : IRequest<List<AvailableVacancyQuestionViewModel>>
    {
        public GetAvailableQuestionsQuery(Guid vacancyId)
        {
            VacancyId = vacancyId;
        }

        public Guid VacancyId { get; }
    }
}
