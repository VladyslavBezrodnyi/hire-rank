using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Vacancies
{
    public class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand, Guid>
    {
        private readonly IStore _store;

        public DeleteVacancyCommandHandler(IStore store)
        {
            _store = store;
        }

        public async Task<Guid> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _store.Vacancies.WithIdAsync(request.Id);

            await _store.DeleteEntityAsync(vacancy, true);

            return vacancy.Id;
        }
    }
}
