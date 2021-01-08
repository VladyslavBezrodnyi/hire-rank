using HireRank.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace HireRank.Application.Queries.Selection
{
    public class GetSelectionQuery : IRequest<List<StudentVacancyViewModel>>
    {
        public Guid Id { get; private set; }

        public GetSelectionQuery(Guid id)
        {
            Id = id;
        }
    }
}
