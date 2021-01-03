using HireRank.Core.StablePairing;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Campaigns
{
    public class CloseCampaignCommandHandler : IRequestHandler<CloseCampaignCommand, Guid>
    {
        private readonly IStore _store;

        public CloseCampaignCommandHandler(IStore store)
        {
            _store = store;
        }

        public async Task<Guid> Handle(CloseCampaignCommand request, CancellationToken cancellationToken)
        {
            var algo = new StableMarriageAlgorith(_store);

            await algo.FindAndSaveAllPairsForCampaignAsync(request.CampaignId);

            return request.CampaignId;
        }
    }
}
