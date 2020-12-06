using HireRank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HireRank.Application.ViewModels
{
    public class StudentVacancyViewModel
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Student Student { get; set; }

        public Guid VacancyId { get; set; }

        public Vacancy Vacancy { get; set; }

        public short Score { get; set; }

        public short Priority { get; set; }

        public bool IsClosed { get; set; }

        public bool IsMatch { get; set; }
    }
}
