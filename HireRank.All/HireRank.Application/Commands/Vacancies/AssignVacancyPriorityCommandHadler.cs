using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Vacancies
{
    public class AssignVacancyPriorityCommandHadler : IRequestHandler<AssignVacancyPriorityCommand, Guid>
    {
        private readonly IStore _store;

        public AssignVacancyPriorityCommandHadler(IStore store)
        {
            _store = store;
        }

        public async Task<Guid> Handle(AssignVacancyPriorityCommand command, CancellationToken cancellationToken)
        {
            var vacancy = await _store.StudentVacancies
                .Where(stv => stv.VacancyId == command.VacancyId && stv.StudentId == command.StudentId)
                .FirstOrDefaultAsync();

            vacancy.Priority = command.Priority;

            await _store.SaveChangesAsync();
            return vacancy.Id;
        }
    }
}
