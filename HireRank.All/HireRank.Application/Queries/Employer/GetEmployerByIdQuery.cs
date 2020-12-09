using HireRank.Application.ViewModels;
using MediatR;
using System;

namespace HireRank.Application.Queries.Employer
{
    public class GetEmployerByIdQuery : IRequest<EmployerViewModel>
    {
        public Guid Id { get; set; }

        public GetEmployerByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
