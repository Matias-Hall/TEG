using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG;
using Spectre.Console;

namespace TEGCMDLineRender
{
    public class CMDAddTroops : ICountryRender, ITroopQuery
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
            Tree possibilities = new Tree("Possible attacks");
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
                        b.AddColumn("Enemy country");
                        b.AddColumn("Troops");
                        foreach (Country other in countryToAttack)
                        {
                            b.AddRow($"[{other.ControllingColor.ToSpectreColor()}]{other.CountryName}[/]", other.ArmySize.ToString());
                        }
                        node.AddNode(b);
                    }
                }
            }

        }
        public Dictionary<Country, int> ChooseCountry(Player player, List<Country> options, int troopsAvailable)
        {
            AnsiConsole.MarkupLine($"[{player.PlayerColor.ToSpectreColor()}]{player.PlayerColor}[/]: add [blue]{troopsAvailable}[/] troops to any country");
            var countryPrompt = new TextPrompt<string>("Country?").InvalidChoiceMessage($"[red]Not a valid country[/]").AddChoices(options.Select(x => x.CountryName));
            int totalTroops = 0;
            Dictionary<Country, int> countriesToAdd = new();
            while (totalTroops < troopsAvailable)
            {
                Country s = CountryManager.CountryFromName(AnsiConsole.Prompt(countryPrompt.HideChoices()));
                int n = AnsiConsole.Prompt(new TextPrompt<int>("Number of troops?").Validate(num =>
                {
                    if (num + totalTroops > troopsAvailable)
                    {
                        return ValidationResult.Error($"[red]Not enough troops available.[/] [blue]{troopsAvailable - totalTroops} left[/]");
                    }
                    else if (num < 0)
                    {
                        return ValidationResult.Error($"[red]Negative numbers not allowed.[/]");
                    }
                    else
                    {
                        return ValidationResult.Success();
                    }
                }));
                totalTroops += n;
                if (countriesToAdd.ContainsKey(s))
                {
                    countriesToAdd[s] += n;
                }
                else
                {
                    countriesToAdd.Add(s, n);
                }
            }
            return countriesToAdd;
        }
    }
}
