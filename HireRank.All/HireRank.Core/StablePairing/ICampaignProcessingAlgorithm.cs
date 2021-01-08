using System;
using System.Threading.Tasks;

namespace HireRank.Core.StablePairing
{
    public interface ICampaignProcessingAlgorithm
    {
        Task FindAndSaveAllPairsForCampaignAsync(Guid campaignId);
    }
}
