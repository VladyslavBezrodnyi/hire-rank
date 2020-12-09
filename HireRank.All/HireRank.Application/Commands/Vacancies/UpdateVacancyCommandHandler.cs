using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Vacancies
{
    public class UpdateVacancyCommandHandler : IRequestHandler<UpdateVacancyCommand, Guid>
    {
        private readonly IStore _store;

        public UpdateVacancyCommandHandler(IStore store)
        {
            _store = store;
        }

        public async Task<Guid> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _store.Vacancies.WithIdAsync(request.Id);

            vacancy.Title = request.Title;
            vacancy.Description = request.Description;
            vacancy.TestSize = request.TestSize;

            await _store.SaveChangesAsync();

            return request.Id;
        }
    }
}
