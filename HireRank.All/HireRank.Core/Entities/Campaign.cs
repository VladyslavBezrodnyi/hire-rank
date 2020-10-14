using System;
using System.Collections.Generic;
using System.Text;

namespace HireRank.Core.Entities
{
    public class Campaign
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<Vacancy> Vacancies = new HashSet<Vacancy>();
    }
}
