using MediatR;
using System;

namespace HireRank.Application.Commands.Vacancies
{
    public class CreateVacancyCommand : IRequest<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int TestSize { get; set; }

        public Guid CampaignId { get; set; }
    }
}
