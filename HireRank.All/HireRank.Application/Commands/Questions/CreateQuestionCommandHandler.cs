using AutoMapper;
using HireRank.Application.Services.Interfaces;
using HireRank.Core.Entities;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Questions
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Guid>
    {
        private readonly IStore _store;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateQuestionCommandHandler(IStore store, ICurrentUserService currentUserService, IMapper mapper)
        {
            _store = store;
            _currentUserService = currentUserService;
            _mapper = mapper;

        }

        public async Task<Guid> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = _mapper.Map<Question>(request);
            question.EmployerId = _currentUserService.GetCurrentUserId();

            await _store.AddEntityAsync(question, saveChanges: true);

            var options = _mapper.Map<List<Option>>(request.Options);
            foreach(var option in options)
            {
                option.QuestionId = question.Id;
                await _store.AddEntityAsync(option);
            }

            await _store.SaveChangesAsync();

            return question.Id;
        }
    }
}
