using HireRank.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace HireRank.Application.Commands.Questions
{
    public class EditQuestionCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string QuestionTag { get; set; }

        public List<OptionViewModel> Options { get; set; }
    }
}
