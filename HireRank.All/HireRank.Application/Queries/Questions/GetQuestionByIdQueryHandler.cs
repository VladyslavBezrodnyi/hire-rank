using AutoMapper;
using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels;
using HireRank.Core.Extensions;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Questions
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionWithOptionsViewModel>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;

        public GetQuestionByIdQueryHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public async Task<QuestionWithOptionsViewModel> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = await _store.Questions.Include(x => x.Options).WithIdAsync(request.Id);

            var questionViewModel = new QuestionWithOptionsViewModel()
            {
                Id = question.Id,
                Text = question.Text,
                QuestionTag = question.QuestionTag,
                Options = _mapper.Map<List<OptionViewModel>>(question.Options)
            };

            return questionViewModel;
        }
    }
}
