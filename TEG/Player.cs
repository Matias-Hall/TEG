using System;
using System.Collections.Generic;
using System.Text;

namespace TEG
{
    public class Player
    {
        public TEGColor PlayerColor { get; init; }
        public List<Country> Countries
        {
            get
            {
                return CountryManager.CountiesFromColor(PlayerColor);
            }
        }
        public Objective PlayerObjective { get; init; }
        public bool IsDestroyed { get; set; }
        public Player(TEGColor c, Objective o)
        {
            PlayerColor = c;
            PlayerObjective = o;
        }
    }
}
