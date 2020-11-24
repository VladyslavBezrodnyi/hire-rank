using System;

namespace HireRank.Application.Queries.Campaigns
{
    public class CampaignViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
