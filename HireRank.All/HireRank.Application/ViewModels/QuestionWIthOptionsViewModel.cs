using System;
using System.Collections.Generic;

namespace HireRank.Application.ViewModels
{
    public class QuestionWithOptionsViewModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string QuestionTag { get; set; }

        public List<OptionViewModel> Options { get; set; }
    }
}
