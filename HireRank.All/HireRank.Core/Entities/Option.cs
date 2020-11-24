using HireRank.Core.Interfaces;
using System;

namespace HireRank.Core.Entities
{
    public class Option : IEntity
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
