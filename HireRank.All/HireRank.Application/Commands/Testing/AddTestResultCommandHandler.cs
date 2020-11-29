using AutoMapper;
using HireRank.Application.Services.Interfaces;
using HireRank.Application.ViewModels;
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


namespace HireRank.Application.Commands.Testing
{
    public class AddTestResultCommandHandler : IRequestHandler<AddTestResultCommand, short>
    {
        private readonly IStore _store;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public AddTestResultCommandHandler(IStore store, ICurrentUserService currentUserService, IMapper mapper)
        {
            _store = store;
            _currentUserService = currentUserService;
            _mapper = mapper;

        }

        public async Task<short> Handle(AddTestResultCommand request, CancellationToken cancellationToken)
        {
            var selectedVacancy = _store.Vacancies.FirstOrDefault(v => v.Id == request.VacancyId);
            Guid studentId = _currentUserService.GetCurrentUserId();

            if (selectedVacancy == null)
            {
                throw new Exception("Vacancy doe not exist.");
            }

            var questionIds = request.Answers.Select(ans => ans.Id).ToList();

            var questionOptionDict = await _store.Questions
                .Include(q => q.Options)
                .Where(q => questionIds.Contains(q.Id))
                .Select(q => new KeyValuePair<Guid, List<Guid>>
                (
                    q.Id, 
                    q.Options.Where(op => op.IsCorrect).Select(op => op.Id).ToList()
                ))
                .ToDictionaryAsync(x => x.Key, x => x.Value);

            int correctOptionCount = questionOptionDict.Sum(q => q.Value.Count());
            int correctUserAnswersCount = request
                .Answers.Sum(ans => ans.ChoosedOptions.Intersect(questionOptionDict[ans.Id]).Count());

            short score = (short)Math.Round(((double)correctUserAnswersCount / correctOptionCount) * 100.0, 0);

         
            short priority = (short)(_store.StudentVacancies
                .Where(x => x.StudentId == studentId && x.Vacancy.CampaignId == selectedVacancy.CampaignId)
                .Count() + 1);

            var newStudentVacancy = new StudentVacancy
            {
                StudentId = _currentUserService.GetCurrentUserId(),
                VacancyId = request.VacancyId,
                Score = score,
                Priority = priority
            };

            return score;
        }
    }
}
