using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Questions
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Guid>
    {
        private readonly IStore _store;

        public DeleteQuestionCommandHandler(IStore store)
        {
            _store = store;
        }

        public async Task<Guid> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _store.Questions.WithIdAsync(request.Id);

            await _store.DeleteEntityAsync(question, saveChanges: true);

            return request.Id;
        }
    }
}
