using System;

namespace HireRank.Core.Entities
{
    public class Option
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
