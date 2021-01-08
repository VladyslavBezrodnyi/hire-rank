using HireRank.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace HireRank.Application.Queries.Questions
{
    public class GetEmployerTestBaseQuery : IRequest<List<TestBaseQuestionViewModel>>
    {
        public Guid VacancyId { get; set; }
    }
}
