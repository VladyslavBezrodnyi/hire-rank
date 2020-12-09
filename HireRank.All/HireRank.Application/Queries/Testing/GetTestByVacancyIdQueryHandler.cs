using AutoMapper;
using AutoMapper.QueryableExtensions;
using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Testing
{
    public class GetTestByVacancyIdQueryHandler : IRequestHandler<GetTestByVacancyIdQuery, TestViewModel>
    {
        private readonly IStore _store;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetTestByVacancyIdQueryHandler(IStore store, ICurrentUserService currentUserService, IMapper mapper)
        {
            _store = store;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<TestViewModel> Handle(GetTestByVacancyIdQuery request, CancellationToken cancellationToken)
        {
            var vacansy = await _store.Vacancies.FirstOrDefaultAsync(v => v.Id == request.Id);
            if (vacansy == null) 
            {
                throw new System.Exception("Vacansy does not exist.");
            }

            var studentVacancy = await _store.StudentVacancies.FirstOrDefaultAsync(sv => sv.VacancyId == vacansy.Id 
            && sv.StudentId == _currentUserService.GetCurrentUserId());

            if(studentVacancy != null)
            {
                return new TestViewModel
                {
                    VacancyId = request.Id,
                    IsPassed = true,
                    Questions = null
                };
            }

            int testSize = vacansy.TestSize;
            var questions = await _store.VacancyQuestions
                .Include(vq => vq.Question)
                .Where(vq => vq.VacancyId == request.Id)
                .Select(vq => vq.Question)
                .ProjectTo<TestQuestionViewModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
            int questionsCount = questions.Length;
            var testQuestions = new List<TestQuestionViewModel>();
            if (testSize < questionsCount)
            {
                var rand = new Random();
                while(testSize > testQuestions.Count)
                {
                    int index = rand.Next(questionsCount);
                    testQuestions.Add(questions[index]);
                }
            }
            else
            {
                testQuestions = questions.ToList();
            }
            return new TestViewModel
            {
                VacancyId = request.Id,
                IsPassed = false,
                Questions = testQuestions
            };
        }
    }
}
