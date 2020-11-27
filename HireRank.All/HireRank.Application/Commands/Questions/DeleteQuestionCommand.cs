using MediatR;
using System;

namespace HireRank.Application.Commands.Questions
{
    public class DeleteQuestionCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
