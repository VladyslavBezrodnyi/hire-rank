using HireRank.Core.StablePairing;
using HireRank.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace HireRank.Infrastructure.StateProcessing
{
    public class CampaignProcessingState : ConcurrentDictionary<Guid, CampaignProcessingStates>, ICampaignProcessingState
    {
        private readonly DbContextOptions<HireRankContext> _dbContextOptions;

        public CampaignProcessingState(DbContextOptions<HireRankContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task<CampaignProcessingStates> CheckStateOfProcessingAsync(Guid id)
        {
            if(!TryGetValue(id, out var currentState))
            {
                currentState = (await IsCampaignClosed(id))
                    ? CampaignProcessingStates.Finished
                    : CampaignProcessingStates.None;

                TryAdd(id, currentState);
            }

            return currentState;
        }

        public void SetProcessingState(Guid id, CampaignProcessingStates state)
        {
            AddOrUpdate(id, state, (id, currentState) => (currentState == CampaignProcessingStates.Finished) ? CampaignProcessingStates.Finished : state);
        }

        private async Task<bool> IsCampaignClosed(Guid id)
        {
            using var context = new HireRankContext(_dbContextOptions);

            return await context.StudentVacancies
                .Where(x => x.Vacancy.CampaignId == id)
                .AnyAsync(x => x.IsClosed);
        }
    }
}
