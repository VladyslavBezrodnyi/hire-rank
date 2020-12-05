using HireRank.Application.Queries.Campaigns;
using System;

namespace HireRank.Application.ViewModels.Shared
{
    public class VacancyViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int TestSize { get; set; }

        public DateTime DateCreated { get; set; }

        public EmployerViewModel Employer { get; set; }

        public CampaignViewModel Campaign { get; set; }
    }
}
