using AutoMapper;
using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Questions
{
    //public class GetEmployerTestBaseQueryHandler : IRequestHandler<GetEmployerTestBaseQuery, List<TestBaseQuestionViewModel>>
    //{
    //    private readonly IStore _store;
    //    private readonly IMapper _mapper;
    //    private readonly ICurrentUserService _currentUserService;

    //    public GetEmployerTestBaseQueryHandler(IStore store, IMapper mapper, ICurrentUserService currentUserService)
    //    {
    //        _store = store;
    //        _mapper = mapper;
    //        _currentUserService = currentUserService;
    //    }

    //    public async Task<List<TestBaseQuestionViewModel>> Handle(GetEmployerTestBaseQuery request, CancellationToken cancellationToken)
    //    {
    //        var employerId = _currentUserService.GetCurrentUserId();
    //        var vacancyTestBase = await _store
    //            .VacancyQuestions
    //            .Where(x => x.VacancyId == request.VacancyId)
    //            .Select(x => x.QuestionId)
    //            .ToListAsync();

    //        var vacancyTestBase = await _store
    //            .Questions
    //            .Where(x => x.)
    //    }
    //}
}
