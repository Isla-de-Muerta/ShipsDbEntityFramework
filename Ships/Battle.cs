using System;
using System.Collections.Generic;

#nullable disable

namespace Ships
{
    public partial class Battle
    {
        public Battle()
        {
            Outcomes = new HashSet<Outcome>();
        }

        public Battle(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Outcome> Outcomes { get; set; }
    }
}
