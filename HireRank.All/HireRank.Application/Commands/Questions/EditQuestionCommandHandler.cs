using AutoMapper;
using HireRank.Application.Services.Interfaces;
using HireRank.Core.Entities;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Questions
{
    public class EditQuestionCommandHandler : IRequestHandler<EditQuestionCommand, Guid>
    {
        private readonly IStore _store;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public EditQuestionCommandHandler(IStore store, ICurrentUserService currentUserService, IMapper mapper)
        {
            _store = store;
            _currentUserService = currentUserService;
            _mapper = mapper;

        }

        public async Task<Guid> Handle(EditQuestionCommand request, CancellationToken cancellationToken)
        {
            var employerId = _currentUserService.GetCurrentUserId();
            var question = await _store.Questions.Where(x => x.EmployerId == employerId).WithIdAsync(request.Id);

            var questionOptions = await _store.Options.Where(x => x.QuestionId == question.Id).ToListAsync();

            await _store.DeleteEntitiesAsync(questionOptions);

            question.Text = request.Text;
            question.QuestionTag = request.QuestionTag;

            var options = _mapper.Map<List<Option>>(request.Options);
            foreach (var option in options)
            {
                option.QuestionId = question.Id;
            }

            await _store.AddEntitiesAsync(options);
            await _store.SaveChangesAsync();

            return request.Id;
        }
    }
}
