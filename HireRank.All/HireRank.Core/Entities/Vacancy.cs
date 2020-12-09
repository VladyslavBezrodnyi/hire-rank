﻿using HireRank.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace HireRank.Core.Entities
{
    public class Vacancy : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int TestSize { get; set; }

        public Guid CampaignId { get; set; }

        public Campaign Campaign { get; set; }

        public Guid EmployerId { get; set; }

        public Employer Employer { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<VacancyQuestion> VacancyQuestions { get; set; } = new HashSet<VacancyQuestion>();

        public ICollection<StudentVacancy> StudentVacancies { get; set; } = new HashSet<StudentVacancy>();
    }
}
