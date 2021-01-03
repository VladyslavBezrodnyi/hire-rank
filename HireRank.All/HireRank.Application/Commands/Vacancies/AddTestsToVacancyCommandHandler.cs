using HireRank.Core.Entities;
using HireRank.Core.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Application.Commands.Vacancies
{
    public class AddTestsToVacancyCommandHandler : IRequestHandler<AddTestsToVacancyCommand, Guid>
    {
        private readonly IStore _store;

        public AddTestsToVacancyCommandHandler(IStore store)
        {
            _store = store;
        }

        public async Task<Guid> Handle(AddTestsToVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancyQuestions = await _store.VacancyQuestions
                .Where(vq => vq.VacancyId == request.VacancyId)
                .ToListAsync();

            await _store.DeleteEntitiesAsync(vacancyQuestions);

            var newVacancyQuestions = new List<VacancyQuestion>();

            foreach(var questionId in request.QuestionIds)
            {
                var newVacancyQuestion = new VacancyQuestion()
                {
                    QuestionId = questionId,
                    VacancyId = request.VacancyId
                };

                newVacancyQuestions.Add(newVacancyQuestion);
            }

            await _store.AddEntitiesAsync(newVacancyQuestions, saveChanges: true);

            return request.VacancyId;
        }
    }
}
