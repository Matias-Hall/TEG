using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TEG
{
    /// <summary>
    /// Converts Objective structs to strings and checks whether objectives have been achieved when called.
    /// </summary>
    static class ObjectiveManager
    {
        public delegate bool Target(Player player);
        static XmlNodeList objectives;
        public static int ObjNum { get => objectives.Count; }
        static ObjectiveManager()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Properties.Resources.Objectives);
            objectives = doc.DocumentElement.ChildNodes;
        }
        public static void AddTargets(Objective objective)
        {
            int commonGoal = int.Parse(objectives.Item(0).FirstChild.Attributes.GetNamedItem("Number").Value); //Number of countries needed to win via common objective.
            Target common = x => x.Countries.Count >= commonGoal;
            objective.LinkCommonTarget(common);
            if (objectives.Item(objective.Id).Attributes.GetNamedItem("Type").Value == "Occupy")
            {
                XmlNodeList xmlTargets = objectives.Item(objective.Id).ChildNodes;
                foreach (XmlNode xmlTarget in xmlTargets)
                {
                    string continent = xmlTarget.Attributes.GetNamedItem("Continent").Value;
                    int number = int.Parse(xmlTarget.Attributes.GetNamedItem("Number").Value);
                    Target target;
                    if (number != -1)
                    {
                        target = x => ContinentManager.PlayerInContinent(x, ContinentManager.ContFromName(continent), number);
                    }
                    else
                    {
                        Continent c = ContinentManager.ContFromName(continent);
                        target = x => ContinentManager.PlayerInContinent(x, c, c.Size);
                    }
                    objective.LinkTarget(target);
                }
            }

        }
        public static string ObjectiveToString(int id)
        {
            StringBuilder b = new StringBuilder();
            XmlNode xmlObj = objectives.Item(id);
            if (xmlObj.Attributes.GetNamedItem("Type").Value == "Occupy")
            {
                XmlNodeList xmlTargets = xmlObj.ChildNodes;
                foreach (XmlNode xmlTarget in xmlTargets)
                {
                    string continent = xmlTarget.Attributes.GetNamedItem("Continent").Value;
                    int number = int.Parse(xmlTarget.Attributes.GetNamedItem("Number").Value);
                    if (number == -1)
                    {
                        b.AppendLine($"-Occupy {continent}.");
                    }
                    else
                    {
                        b.AppendLine($"-Occupy {number} countries from {continent}.");
                    }
                }
            }
            else
            {
                b.AppendLine($"Destroy {xmlObj.FirstChild.Attributes.GetNamedItem("Color").Value} army.");
            }
            return b.ToString();
        }
        public static string ObjectiveToString(Objective objective)
        {
            return ObjectiveToString(objective.Id);
        }
    }
}
