using HireRank.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace HireRank.Application.Commands.Testing
{
    public class AddTestResultCommand : IRequest<short>
    {
        public Guid VacancyId { get; set; }
        public List<PassedTestQuestionViewModel> Answers { get; set; }
    }
}
