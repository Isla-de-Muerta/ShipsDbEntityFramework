using System;
using System.Collections.Generic;

#nullable disable

namespace Ships
{
    public partial class Class
    {
        public Class()
        {
            Ships = new HashSet<Ship>();
        }

        public Class(string @class, string type, string country, byte numGuns, int bore, int displacement)
        {
            Class1 = @class;
            Type = type;
            Country = country;
            NumGuns = numGuns;
            Bore = bore;
            Displacement = displacement;
        }

        public string Class1 { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public byte? NumGuns { get; set; }
        public float? Bore { get; set; }
        public int? Displacement { get; set; }

        public virtual ICollection<Ship> Ships { get; set; }
    }
}
