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
        public (bool regroup, Country from, Country to) ChooseFromToCountry(Player player, Dictionary<Country, List<Country>> options)
        {
            AnsiConsole.MarkupLine($"[{player.PlayerColor.ToSpectreColor()}]{player.PlayerColor}[/] regroup!");
            if (AnsiConsole.Confirm("Regroup?"))
            {
                Country from = CountryManager.CountryFromName(AnsiConsole.Prompt(new TextPrompt<string>("Country to take from?").AddChoices(options.Keys.Select(x => x.CountryName))));
                Country to = CountryManager.CountryFromName(AnsiConsole.Prompt(new TextPrompt<string>("Country to send to?").AddChoices(from.Neighbors.Where(x => x.ControllingColor == player.PlayerColor).Select(x => x.CountryName))));
                return (true, from, to);
            }
            else
            {
                return (false, null, null);
            }
        }
        public int QueryTransferOfTroops(int possibleTroops)
        {
            return AnsiConsole.Prompt(new TextPrompt<int>("Number of troops to transfer?").Validate(num =>
            {
                if (num > possibleTroops)
                {
                    return ValidationResult.Error($"[red]Not enough troops available.[/] [blue]{possibleTroops} left[/]");
                }
                else if (num < 0)
                {
                    return ValidationResult.Error($"[red]Must transfer at least 1 troop.[/]");
                }
                else
                {
                    return ValidationResult.Success();
                }
            }));
        }
    }
}
