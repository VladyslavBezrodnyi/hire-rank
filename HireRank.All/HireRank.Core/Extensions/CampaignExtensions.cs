using HireRank.Core.Entities;
using HireRank.Core.StablePairing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HireRank.Core.Extensions
{
    public static class CampaignExtensions
    {
        public static IQueryable<Campaign> Active(this IQueryable<Campaign> campaigns)
        {
            return campaigns.Where(campaign => campaign.EndDate > DateTime.Now 
                                            && campaign.StartDate <= DateTime.Now);
        }
    }
}
