using System;
using System.Collections.Generic;
using System.Text;

namespace HireRank.Application.ViewModels
{
    public class TestQuestionViewModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string QuestionTag { get; set; }

        public List<TestOptionViewModel> Options { get; set; }
    }
}
