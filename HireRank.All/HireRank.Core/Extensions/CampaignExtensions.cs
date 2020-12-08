using HireRank.Core.Entities;
using System;
using System.Linq;

namespace HireRank.Core.Extensions
{
    public static class CampaignExtensions
    {
        public static IQueryable<Campaign> ActiveCampaigns(this IQueryable<Campaign> campaigns)
        {
            return campaigns.Where(campaign => campaign.EndDate > DateTime.Now && campaign.StartDate <= DateTime.Now);
        }
    }
}
