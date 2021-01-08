using HireRank.Core.StablePairing;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Campaigns
{
    public class CloseCampaignCommandHandler : IRequestHandler<CloseCampaignCommand, Guid>
    {
        private readonly ICampaignProcessingAlgorithm _campaignProcessingAlgorithm;

        public CloseCampaignCommandHandler(ICampaignProcessingAlgorithm campaignProcessingAlgorithm)
        {
            _campaignProcessingAlgorithm = campaignProcessingAlgorithm;
        }

        public async Task<Guid> Handle(CloseCampaignCommand request, CancellationToken cancellationToken)
        {
            await _campaignProcessingAlgorithm.FindAndSaveAllPairsForCampaignAsync(request.CampaignId);

            return request.CampaignId;
        }
    }
}
