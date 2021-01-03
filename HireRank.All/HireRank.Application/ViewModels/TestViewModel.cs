using System;
using System.Collections.Generic;
using System.Text;

namespace HireRank.Application.ViewModels
{
    public class TestViewModel
    {
        public Guid VacancyId { get; set; }
        public bool IsPassed { get; set; }
        public List<TestQuestionViewModel> Questions { get; set; }
    }
}
