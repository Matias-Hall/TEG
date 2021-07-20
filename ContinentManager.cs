using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TEG
{
    static class ContinentManager
    {
        public static List<Continent> Continents { get => continents; }
        private static List<Continent> continents;
        static ContinentManager()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Properties.Resources.Countries);
            XmlNodeList conts = doc.DocumentElement.ChildNodes;
            List<Continent> c = new List<Continent>();
            foreach (XmlNode cont in conts)
            {
                c.Add(new Continent(cont.Attributes.GetNamedItem("Name").Value, int.Parse(cont.Attributes.GetNamedItem("Bonus").Value), cont.ChildNodes.Count));
            }
            continents = c;
        }
        public static bool PlayerInContinent(Player player, Continent continent, int needed) //Returns whether the player has at least the needed number of countries in the given continent
        {
            return player.Countries.Where(x => x.Continent == continent).Count() >= needed;
        }
        public static Continent ContFromName(string name)
        {
            return Continents.Where(x => x.Name == name).ToList()[0];
        }
    }
}
