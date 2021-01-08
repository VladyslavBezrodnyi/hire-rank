using HireRank.Core.Entities;
using System;
using System.Linq;

namespace HireRank.Core.Extensions
{
    public static class QuestionExtensions
    {
        public static IQueryable<Question> EmployerQuestions(this IQueryable<Question> questions, Guid employerId)
        {
            return questions.Where(q => q.EmployerId == employerId);
        }
    }
}
