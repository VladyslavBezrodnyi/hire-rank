using System;

namespace HireRank.Application.ViewModels
{
    public class StudentViewModel
    {
        public string Email { get; set; }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string UniversityName { get; set; }

        public string Major { get; set; }
    }
}
