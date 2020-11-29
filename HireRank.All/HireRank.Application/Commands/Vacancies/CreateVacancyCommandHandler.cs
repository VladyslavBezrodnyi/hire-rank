using AutoMapper;
using HireRank.Application.Services.Interfaces;
using HireRank.Core.Entities;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Vacancies
{
    public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, Guid>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateVacancyCommandHandler(IStore store, IMapper mapper, ICurrentUserService currentUserService)
        {
            _store = store;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = _mapper.Map<Vacancy>(request);
            var employerId = _currentUserService.GetCurrentUserId();

            vacancy.DateCreated = DateTime.Now;

            vacancy.EmployerId = employerId;

            await _store.AddEntityAsync(vacancy, saveChanges: true);

            return vacancy.Id;
        }
    }
}
