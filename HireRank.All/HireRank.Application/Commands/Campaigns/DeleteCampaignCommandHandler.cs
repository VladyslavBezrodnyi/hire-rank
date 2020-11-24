using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Campaigns
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, Guid>
    {
        private readonly IStore _store;

        public DeleteCampaignCommandHandler(IStore store)
        {
            _store = store;
        }

        public async Task<Guid> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _store.Campaigns.WithIdAsync(request.Id);

            await _store.DeleteEntityAsync(campaign, saveChanges: true);

            return request.Id;
        }
    }
}
