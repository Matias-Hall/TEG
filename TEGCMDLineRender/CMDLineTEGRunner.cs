using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using TEG;

namespace TEGCMDLineRender
{
    class CMDLineTEGRunner : BaseTEGRunner
    {
        public CMDLineTEGRunner(int playerNum) : base(playerNum) { }
        protected override Dictionary<Country, int> ContinentBonus(Player player, Continent continent, int troopsAvailable)
        {
            AnsiConsole.MarkupLine($"[{player.PlayerColor.ToSpectreColor()}]{player.PlayerColor}[/]: add [blue]{troopsAvailable}[/] troops to countries in {continent.Name}");
            #region Creates country prompt and table
            Table t = new Table();
            var countryPrompt = new TextPrompt<string>("Country?").InvalidChoiceMessage($"[red]Not a valid country[/]");
            t.AddColumn("Countries available");
            t.AddColumn("Troops");
            foreach (var c in player.Countries.Where(x => x.Continent == continent))
            {
                t.AddRow(c.CountryName, c.ArmySize.ToString());
                countryPrompt = countryPrompt.AddChoice(c.CountryName);
            }
            AnsiConsole.Render(t);
            #endregion
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
