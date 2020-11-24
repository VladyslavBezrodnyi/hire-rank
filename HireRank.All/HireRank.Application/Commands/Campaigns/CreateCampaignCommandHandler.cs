using HireRank.Application.Services.Interfaces;
using HireRank.Core.Entities;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Campaigns
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Guid>
    {
        private readonly IStore _store;
        private readonly ICurrentUserService _currentUserService;

        public CreateCampaignCommandHandler(IStore store, ICurrentUserService currentUserService)
        {
            _store = store;
            _currentUserService = currentUserService;

        }

        public async Task<Guid> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = new Campaign()
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                AdminId = _currentUserService.GetCurrentUserId()
            };

            var addedEntity = await _store.AddEntityAsync(campaign, saveChanges: true);
            return addedEntity.Id;
        }
    }
}
