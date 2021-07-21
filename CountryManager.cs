using ArrayExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace TEG
{
    public static class CountryManager
    {
        private static Dictionary<string, Country> countries = new Dictionary<string, Country>();
        public static Country CountryFromName(string name)
        {
            return countries.GetValueOrDefault(name) ?? throw new ArgumentOutOfRangeException("Country not found.");
        }
        public static List<Country> CountiesFromColor(TEGColor color)
        {
            return (from country in countries
                    where country.Value.ControllingColor == color
                    select country.Value).ToList();
        }
        public static void ResetReceivedTroops() //Resets the ReceivedTroops property on all countries to false. Meant to be used at the end of a turn.
        {
            countries.Values.ToList().ForEach(x => x.ReceivedTroops = false);
        }
        static CountryManager()
        {
            XmlDocument countriesDoc = new XmlDocument();
            countriesDoc.LoadXml(Properties.Resources.Countries);
            XmlElement world = countriesDoc.DocumentElement;
            XmlNodeList continents = world.ChildNodes;
            foreach (XmlNode continent in continents)
            {
                foreach (XmlNode country in continent.ChildNodes)
                {
                    Country a = new Country(country.Attributes.GetNamedItem("Name").Value, ContinentManager.ContFromName(continent.Attributes.GetNamedItem("Name").Value), TEGColor.Black);
                    countries.Add(a.CountryName, a);
                }
            }
            //First needed to create all countries before assigning neighbors, therefore foreach loops are repeated twice.
            foreach (XmlNode continent in continents)
            {
                foreach (XmlNode country in continent.ChildNodes)
                {
                    countries.GetValueOrDefault(country.Attributes.GetNamedItem("Name").Value).Neighbors = (from c in country.Attributes.GetNamedItem("Neighbors").Value.Split(",")
                                                                                                            select CountryFromName(c)).ToList();
                }
            }
        }
        public static List<Player> SortCountries(List<Player> players)
        {
            List<Country> c = countries.Values.ToList();
            c.Shuffle();
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < 50 / players.Count; j++)
                {
                    c[j + i * 50 / players.Count].ControllingColor = players[i].PlayerColor;
                }
            }
            if (50 % players.Count != 0)
            {
                List<Player> shuffledPlayers = (List<Player>)players.DeepCopy();
                shuffledPlayers.Shuffle();

                c[^1].ControllingColor = shuffledPlayers[0].PlayerColor;
                c[(int)Math.Ceiling(players.Count / 2.0) * (50 / players.Count)].ControllingColor = shuffledPlayers[1].PlayerColor;

            }
            return players;
        }
    }
}
