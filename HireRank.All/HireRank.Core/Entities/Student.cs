using System;
using System.Collections.Generic;

namespace HireRank.Core.Entities
{
    public class Student : User
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string UniversityName { get; set; }

        public string Major { get; set; }

        public ICollection<StudentVacancy> StudentVacancies { get; set; } = new HashSet<StudentVacancy>();
    }
}
