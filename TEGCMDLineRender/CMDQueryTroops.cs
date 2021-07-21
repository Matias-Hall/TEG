using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG;
using Spectre.Console;

namespace TEGCMDLineRender
{
    class CMDQueryTroops : ITroopQuery
    {
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
