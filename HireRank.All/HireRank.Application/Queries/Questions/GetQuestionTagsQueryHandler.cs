using HireRank.Application.Services.Interfaces;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Questions
{
    public class GetQuestionTagsQueryHandler : IRequestHandler<GetQuestionTagsQuery, List<string>>
    {
        private readonly IStore _store;
        private readonly ICurrentUserService _currentUserService;
        private readonly List<string> _defaultTags;

        public GetQuestionTagsQueryHandler(IStore store, ICurrentUserService currentUserService)
        {
            _store = store;
            _currentUserService = currentUserService;
            _defaultTags = new List<string>()
            {
                "C#",
                "js",
                "ASP.NET Core",
                "java",
                "C++",
                "python",
                "unity",
                "project managment",
                "unity",
                "android",
                "QA"
            };
        }

        public async Task<List<string>> Handle(GetQuestionTagsQuery request, CancellationToken cancellationToken)
        {
            var employerId = _currentUserService.GetCurrentUserId();

            var employerTags = await _store.Questions
                .Where(x => x.EmployerId == employerId)
                .Select(x => x.QuestionTag)
                .ToListAsync();

            employerTags.AddRange(_defaultTags);
            return employerTags.Distinct().ToList();
        }
    }
}
