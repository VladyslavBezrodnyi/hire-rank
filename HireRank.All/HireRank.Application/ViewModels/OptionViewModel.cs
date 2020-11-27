using System;

namespace HireRank.Application.ViewModels
{
    public class OptionViewModel
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
