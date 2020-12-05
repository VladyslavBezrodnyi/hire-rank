using HireRank.Core.Entities;
using System;

namespace HireRank.Core.Extensions
{
    public static class CampaignExtensions
    {
        public static bool IsActive(this Campaign campaign)
        {
            return campaign.EndDate > DateTime.Now && campaign.StartDate <= DateTime.Now;
        }
    }
}
