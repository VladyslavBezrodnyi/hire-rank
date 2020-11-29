using System;
using System.Collections.Generic;
using System.Text;

namespace HireRank.Application.ViewModels
{
    public class PassedTestQuestionViewModel
    {
        public Guid Id { get; set; }
        public List<Guid> ChoosedOptions { get; set; }
    }
}
