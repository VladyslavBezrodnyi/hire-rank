using HireRank.Application.Filtering;
using HireRank.Application.ViewModels;
using HireRank.Common.Extensions;
using HireRank.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HireRank.Application.Queries.Questions
{
    public class GetEmployerQuestionQuery : IRequest<PagedResult<QuestionViewModel>>, IQuery<Question>
    {
        public List<string> Tags { get; set; }

        public string Text { get; set; }

        public PagingViewModel Paging { get; set; }

        public SortingViewModel Sorting { get; set; }

        public Expression<Func<Question, bool>> GetFilteringPredicate()
        {
            Expression<Func<Question, bool>> filteringExpression = null;

            if (!string.IsNullOrEmpty(Text))
            {
                TryAddPredicate(ref filteringExpression, question => question.Text.Contains(Text));
            }

            if (Tags != null && Tags.Any())
            {
                TryAddPredicate(ref filteringExpression, question => Tags.Any(x => x == question.QuestionTag));
            }

            return filteringExpression;
        }

        private void TryAddPredicate(ref Expression<Func<Question, bool>> expression,
                                        Expression<Func<Question, bool>> predicate)
        {

            expression = expression != null ? expression.AndAlso(predicate) : predicate;

        }
    }
}
