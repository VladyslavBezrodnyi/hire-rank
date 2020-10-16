using System;
using System.Collections.Generic;

namespace HireRank.Core.Entities
{
    public class Admin : User
    {
        public string Name { get; set; }

        public string ContactPhone { get; set; }

        public ICollection<Campaign> Campaigns { get; set; } = new HashSet<Campaign>();
    }
}
