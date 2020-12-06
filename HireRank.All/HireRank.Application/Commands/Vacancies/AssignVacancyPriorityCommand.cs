using MediatR;
using System;

namespace HireRank.Application.Commands.Vacancies
{
    public class AssignVacancyPriorityCommand : IRequest<Guid>
    {
        public Guid StudentId { get; set; }

        public Guid VacancyId { get; set; }

        public short Priority { get; set; }
    }
}
