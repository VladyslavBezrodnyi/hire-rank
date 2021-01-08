using HireRank.Application.ViewModels.Shared;
using System;

namespace HireRank.Application.ViewModels
{
    public class StudentVacancyViewModel
    {
        public Guid Id { get; set; }

        public StudentViewModel Student { get; set; }

        public VacancyViewModel Vacancy { get; set; }

        public short Score { get; set; }

        public short Priority { get; set; }

        public bool IsClosed { get; set; }

        public bool IsMatch { get; set; }

        public int Order { get; set; }
    }
}
