using System;
using System.Threading.Tasks;

namespace HireRank.Core.StablePairing
{
    public interface ICampaignProcessingState
    {
        void SetProcessingState(Guid id, CampaignProcessingStates state);

        Task<CampaignProcessingStates> CheckStateOfProcessingAsync(Guid id);
    }

    public enum CampaignProcessingStates
    {
        None,
        Started,
        Finished,
    }
}
