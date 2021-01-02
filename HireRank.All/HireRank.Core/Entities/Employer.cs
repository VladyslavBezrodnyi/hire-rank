using System;
using System.Collections.Generic;

namespace HireRank.Core.Entities
{
    public class Employer : User
    {
        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }

        public string CompanyAddress { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string SiteUrl { get; set; }

        public bool IsConfirmed { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; } = new HashSet<Vacancy>();

        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
    }
}
