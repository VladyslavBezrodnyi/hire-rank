using System;

namespace HireRank.Application.Commands.Account
{
    public class StudentRegisterCommand : RegisterCommand
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string UniversityName { get; set; }

        public string Major { get; set; }
    }
}
