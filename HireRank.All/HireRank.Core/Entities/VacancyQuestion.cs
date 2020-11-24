using HireRank.Core.Interfaces;
using System;

namespace HireRank.Core.Entities
{
    public class VacancyQuestion : IEntity
    {
        public Guid Id { get; set; }

        public Guid VacancyId { get; set; }

        public Vacancy Vacancy { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
