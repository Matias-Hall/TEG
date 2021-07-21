using System;
using System.Collections.Generic;
using System.Text;

namespace TEG
{
    public class Country
    {
        public int ArmySize { get => armySize; }
        private int armySize;
        public TEGColor ControllingColor
        {
            get { return color; }
            set
            {
                color = value; //might change
            }
        }
        private TEGColor color;
        public Continent Continent { get; init; }
        public string CountryName { get; init; }
        public List<Country> Neighbors { get; set; }
        /// <summary>
        /// Used for the regrouping of troops.
        /// </summary>
        public bool ReceivedTroops { get; set; }
        public Country(string name, Continent continent, TEGColor color)
        {
            CountryName = name;
            Continent = continent;
            ControllingColor = color;
            armySize = 1;
        }
        public void AddTroops(int n)
        {
            armySize += n;
        }
    }
}
