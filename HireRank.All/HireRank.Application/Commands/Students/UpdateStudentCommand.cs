using HireRank.Application.ViewModels;
using MediatR;
using System;

namespace HireRank.Application.Commands.Students
{
    public class UpdateStudentCommand : IRequest<StudentViewModel>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string UniversityName { get; set; }

        public string Major { get; set; }
    }
}
