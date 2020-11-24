using HireRank.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace HireRank.Core.Entities
{
    public class Campaign : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid AdminId { get; set; }

        public Admin Admin { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; } = new HashSet<Vacancy>();
    }
}
