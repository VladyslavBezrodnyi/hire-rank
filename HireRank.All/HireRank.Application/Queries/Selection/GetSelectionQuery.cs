using AutoMapper;
using HireRank.Application.ViewModels;
using HireRank.Core.Store;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HireRank.Application.Queries.Selection
{
    public class GetSelectionQuery : IRequest<List<StudentVacancyViewModel>>
    {
        public Guid Id { get; private set; }

        public GetSelectionQuery(Guid id)
        {
            Id = id;
        }
    }
}
