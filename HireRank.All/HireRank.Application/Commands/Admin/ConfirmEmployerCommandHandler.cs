using HireRank.Application.Services.Interfaces;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Admin
{
    public class ConfirmEmployerCommandHandler : IRequestHandler<ConfirmEmployerCommand, bool>
    {
        private readonly IStore _store;
        private readonly ICurrentUserService _currentUserService;

        public ConfirmEmployerCommandHandler(IStore store, ICurrentUserService currentUserService)
        {
            _store = store;
            _currentUserService = currentUserService;

        }

        public async Task<bool> Handle(ConfirmEmployerCommand request, CancellationToken cancellationToken)
        {
            var employer = await _store.Employers.WithIdAsync(request.Id);
            employer.IsConfirmed = !employer.IsConfirmed;
            await _store.SaveChangesAsync();

            return employer.IsConfirmed;
        }
    }
}
