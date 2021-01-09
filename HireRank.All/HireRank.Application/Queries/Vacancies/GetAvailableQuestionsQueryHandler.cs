using AutoMapper;
using AutoMapper.QueryableExtensions;
using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels;
using HireRank.Core.Extensions;
using HireRank.Core.StablePairing;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetAvailableQuestionsQueryHandler : IRequestHandler<GetAvailableQuestionsQuery,
                                                        List<AvailableVacancyQuestionViewModel>>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetAvailableQuestionsQueryHandler(IStore store, IMapper mapper, ICurrentUserService currentUserService)
        {
            _store = store;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<AvailableVacancyQuestionViewModel>> Handle(GetAvailableQuestionsQuery request, CancellationToken cancellationToken)
        {
            var employerId = _currentUserService
                .GetCurrentUserId();

            var vacancyQuestionIds = await _store
                .VacancyQuestions
                .Where(vq => vq.VacancyId == request.VacancyId)
                .Select(vq => vq.QuestionId)
                .ToListAsync();

            var availableQuestions = await _store.Questions
                .EmployerQuestions(employerId)
                .ProjectTo<AvailableVacancyQuestionViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            availableQuestions.ForEach(question =>
            {
                question.Selected = vacancyQuestionIds.Contains(question.Id);
            });

            return availableQuestions;
        }
    }
}
