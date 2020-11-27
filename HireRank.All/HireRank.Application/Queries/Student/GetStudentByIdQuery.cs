using HireRank.Application.ViewModels;
using MediatR;
using System;

namespace HireRank.Application.Queries.Student
{
    public class GetStudentByIdQuery : IRequest<StudentViewModel>
    {
        public Guid Id { get; set; }

        public GetStudentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
