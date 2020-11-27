using HireRank.Application.ViewModels;
using MediatR;
using System;

namespace HireRank.Application.Queries.Questions
{
    public class GetQuestionByIdQuery : IRequest<QuestionWithOptionsViewModel>
    {
        public Guid Id { get; set; }
    }
}
