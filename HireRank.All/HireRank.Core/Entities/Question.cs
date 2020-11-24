using HireRank.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace HireRank.Core.Entities
{
    public class Question : IEntity
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string QuestionTag { get; set; }

        public Guid EmployerId { get; set; }

        public Employer Employer { get; set; }

        public ICollection<VacancyQuestion> VacancyQuestions { get; set; } = new HashSet<VacancyQuestion>();

        public ICollection<Option> Options { get; set; } = new HashSet<Option>();
    }
}
