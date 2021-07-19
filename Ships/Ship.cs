using System;
using System.Collections.Generic;

#nullable disable

namespace Ships
{
    public partial class Ship
    {
        public Ship(string name, string @class, short launched)
        {
            Name = name;
            Class = @class;
            Launched = launched;
        }
        public string Name { get; set; }
        public string Class { get; set; }
        public short? Launched { get; set; }

        public virtual Class ClassNavigation { get; set; }
    }
}
