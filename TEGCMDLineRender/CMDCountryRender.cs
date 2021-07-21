using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG;
using Spectre.Console;

namespace TEGCMDLineRender
{
    public class CMDCountryRender : ICountryRender
    {
        public void RenderCountries(Player player, List<Country> countries)
        {
            Table t = new Table();
            t.AddColumn("Countries available");
            t.AddColumn("Troops");
            foreach (var c in player.Countries)
            {
                t.AddRow(c.CountryName, c.ArmySize.ToString());
            }
            AnsiConsole.Render(t);
        }
        public void RenderFromToCountries(Player player, Dictionary<Country, List<Country>> countries)
        {
            Tree possibilities = new Tree("Possible moves");
            foreach (var c in countries)
            {
                if (c.Key.ArmySize > 1)
                {
                    List<Country> countryToAttack = c.Value;
                    if (countryToAttack.Count > 0)
                    {
                        Table a = new Table().AddColumn("").AddColumn("").HideHeaders();
                        a.AddRow(c.Key.CountryName, c.Key.ArmySize.ToString());
                        var node = possibilities.AddNode(a);
                        Table b = new Table().MinimalBorder();
                        b.AddColumn("Target country");
                        b.AddColumn("Troops");
                        foreach (Country other in countryToAttack)
                        {
                            b.AddRow($"[{other.ControllingColor.ToSpectreColor()}]{other.CountryName}[/]", other.ArmySize.ToString());
                        }
                        node.AddNode(b);
                    }
                }
            }
            AnsiConsole.Render(possibilities);

        }
    }
}
