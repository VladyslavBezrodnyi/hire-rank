using AutoMapper;
using HireRank.Application.Filtering;
using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels;
using HireRank.Core.Entities;
using HireRank.Core.Store;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Questions
{
    public class GetEmployerQuestionQueryHandler : IRequestHandler<GetEmployerQuestionQuery, PagedResult<QuestionViewModel>>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetEmployerQuestionQueryHandler(IStore store, IMapper mapper, ICurrentUserService currentUserService)
        {
            _store = store;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<QuestionViewModel>> Handle(GetEmployerQuestionQuery request, CancellationToken cancellationToken)
        {
            var employerId = _currentUserService.GetCurrentUserId();

            return await _store.Questions
                .Where(x => x.EmployerId == employerId)
                .ApplyQueryAsync<Question, QuestionViewModel>(request, _mapper.ConfigurationProvider);
        }
    }
}
