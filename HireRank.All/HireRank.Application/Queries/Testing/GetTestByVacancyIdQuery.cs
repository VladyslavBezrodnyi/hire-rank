using HireRank.Application.ViewModels;
using MediatR;
using System;

namespace HireRank.Application.Queries.Testing
{
    public class GetTestByVacancyIdQuery : IRequest<TestViewModel>
    {
        public Guid Id { get; set; }

        public GetTestByVacancyIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
