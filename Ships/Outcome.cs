using System;
using System.Collections.Generic;

#nullable disable

namespace Ships
{
    public partial class Outcome
    {
        public Outcome(string ship, string battle, string result)
        {
            Ship = ship;
            Battle = battle;
            Result = result;
        }
        public string Ship { get; set; }
        public string Battle { get; set; }
        public string Result { get; set; }

        public virtual Battle BattleNavigation { get; set; }
    }
}
