using HireRank.Core.Entities;
using HireRank.Core.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HireRank.Core.StablePairing
{
    public class StableMarriageAlgorithm : ICampaignProcessingAlgorithm
    {
        private readonly IStore _store;
        private readonly ICampaignProcessingState _processingState;

        public StableMarriageAlgorithm(IStore store, ICampaignProcessingState processingState)
        {
            _store = store;
            _processingState = processingState;
        }

        public async Task FindAndSaveAllPairsForCampaignAsync(Guid campaignId)
        {
            _processingState.SetProcessingState(campaignId, CampaignProcessingStates.Started);

            var studentVacancies =  await 
                _store.StudentVacancies
                .Where(x => x.Vacancy.CampaignId == campaignId)
                .ToListAsync();

            var studentIds = studentVacancies.Select(x => x.StudentId).Distinct().ToList();
            var vacancyIds = studentVacancies.Select(x => x.VacancyId).Distinct().ToList();

            AddFakeStudentVacancies(ref studentVacancies, studentIds, vacancyIds);

            Dictionary<Guid, bool> vacancyAvailables = vacancyIds.ToDictionary(key => key, value => true);

            Dictionary<Guid, Guid?> studentChoices = studentIds.ToDictionary(key => key, value => (Guid?)null);

            var studentPreferances = GetStudentPreferances(studentVacancies, studentIds);
            var vacancyPreferances = GetVacancyPreferances(studentVacancies, vacancyIds);
            int freeVacancyCount = vacancyIds.Count;

            while(freeVacancyCount > 0)
            {
                var vacancyId = GetFreeVacancy(vacancyAvailables);
                if (!vacancyId.HasValue)
                    break;

                foreach(var studentId in vacancyPreferances[vacancyId.Value])
                {
                    if (!vacancyAvailables[vacancyId.Value])
                        break;

                    if(studentChoices[studentId] == null)
                    {
                        studentChoices[studentId] = vacancyId;
                        vacancyAvailables[vacancyId.Value] = false;
                        --freeVacancyCount;
                    }
                    else
                    {
                        var studentCurrentChoice = studentChoices[studentId];
                        if (!PreferMoreThan(studentPreferances[studentId], vacancyId.Value, studentCurrentChoice.Value))
                        {
                            studentChoices[studentId] = vacancyId;
                            vacancyAvailables[vacancyId.Value] = false;
                            vacancyAvailables[studentCurrentChoice.Value] = true;
                        }
                    }
                }
            }

            foreach(var studentVacancy in studentVacancies)
            {
                if(studentVacancy.Priority != -1)
                {
                    studentVacancy.IsClosed = true;
                }

                if(studentChoices[studentVacancy.StudentId] == studentVacancy.VacancyId)
                {
                    studentVacancy.IsMatch = true;
                }
            }

            await _store.SaveChangesAsync();

            _processingState.SetProcessingState(campaignId, CampaignProcessingStates.Finished);
        }

        private Guid? GetFreeVacancy(Dictionary<Guid, bool> vacancyAvailables)
        {
            foreach(var vacancy in vacancyAvailables.Keys)
            {
                if (vacancyAvailables[vacancy])
                    return vacancy;
            }
            return null;
        }

        private void AddFakeStudentVacancies(ref List<StudentVacancy> studentVacancies, List<Guid> studentIds, List<Guid> vacancyIds)
        {
            foreach (var vacancyId in vacancyIds)
            {
                foreach (var studentId in studentIds)
                {
                    if (!studentVacancies.Any(x => x.VacancyId == vacancyId && x.StudentId == studentId))
                    {
                        studentVacancies.Add(new StudentVacancy()
                        {
                            StudentId = studentId,
                            VacancyId = vacancyId,
                            Priority = -1,
                            Score = -1
                        });
                    }
                }
            }
        }

        private bool PreferMoreThan(List<Guid> orderedVacancies, Guid firstVacancyId, Guid secondVacancyId)
        {
            foreach(var vacancyId in orderedVacancies)
            {
                if (vacancyId == firstVacancyId)
                    return true;

                if (vacancyId == secondVacancyId)
                    return false;
            }
            return false;
        }

        private Dictionary<Guid, List<Guid>> GetStudentPreferances(List<StudentVacancy> studentVacancies, List<Guid> studentIds)
        {
            var preferes = studentIds
                .ToDictionary(key => key, studentId => studentVacancies
                    .Where(x => x.StudentId == studentId)
                    .OrderByDescending(x => x.Priority)
                    .Select(x => x.VacancyId)
                    .ToList());
            //.ThenBy(x => x.DateCreated)

            return preferes;
        }

        private Dictionary<Guid, List<Guid>> GetVacancyPreferances(List<StudentVacancy> studentVacancies, List<Guid> vacancyIds)
        {
            var preferes = vacancyIds
                .ToDictionary(key => key, vacancyId => studentVacancies
                    .Where(x => x.VacancyId == vacancyId)
                    .OrderByDescending(x => x.Score)
                    .Select(x => x.StudentId)
                    .ToList());
            //.ThenBy(x => x.DateCreated)

            return preferes;
        }
    }
}
