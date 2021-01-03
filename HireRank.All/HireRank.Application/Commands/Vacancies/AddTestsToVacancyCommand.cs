using MediatR;
using System;
using System.Collections.Generic;

namespace HireRank.Application.Commands.Vacancies
{
    public class AddTestsToVacancyCommand : IRequest<Guid>
    {
        public AddTestsToVacancyCommand(Guid id, List<Guid> questionIds)
        {
            VacancyId = id;
            QuestionIds = questionIds;
        }

        public Guid VacancyId { get; set;  }

        public List<Guid> QuestionIds { get; set; }
    }
}
