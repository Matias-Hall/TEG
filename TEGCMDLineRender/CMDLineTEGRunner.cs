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
        protected override void ShowObjective(Player player)
        {
            AnsiConsole.MarkupLine($"[{player.PlayerColor.ToSpectreColor()}]{player.PlayerColor}[/] objective:");
            Console.WriteLine(ObjectiveManager.ObjectiveToString(player.PlayerObjective));
            Console.Write("Delete secret");
            Console.ReadKey(true);
            Console.Clear();
        }
        protected override (Country from, Country to, int troopNumber) RegroupTroops(Player player)
        {
            AnsiConsole.MarkupLine($"[{player.PlayerColor.ToSpectreColor()}]{player.PlayerColor}[/] regroup!");
            #region Creates display of possible regoupings
            Tree possibilities = new Tree("Possible regrouping");
            List<string> possibleCountriesFrom = new List<string>();
            foreach (Country c in player.Countries)
            {
                List<Country> neighbors = c.Neighbors.Where(x => x.ControllingColor == player.PlayerColor).ToList();
                if (c.ArmySize > 1 && !c.ReceivedTroops && neighbors.Count > 0)
                {
                    possibleCountriesFrom.Add(c.CountryName);
                    Table a = new Table().AddColumn("").AddColumn("").HideHeaders();
                    a.AddRow(c.CountryName, c.ArmySize.ToString());
                    TreeNode node = possibilities.AddNode(a);
                    Table b = new Table().AddColumn("To").AddColumn("Current Troops").MinimalBorder();
                    foreach (Country d in neighbors)
                    {
                        b.AddRow(d.CountryName, d.ArmySize.ToString());
                    }
                    node.AddNode(b);
                }
            }
            #endregion
            if (possibleCountriesFrom.Count > 0)
            {
                AnsiConsole.Render(possibilities);
                if (AnsiConsole.Confirm("Regroup?"))
                {
                    Country from = CountryManager.CountryFromName(AnsiConsole.Prompt(new TextPrompt<string>("Country to take from?").AddChoices(possibleCountriesFrom)));
                    Country to = CountryManager.CountryFromName(AnsiConsole.Prompt(new TextPrompt<string>("Country to send to?").AddChoices(from.Neighbors.Where(x => x.ControllingColor == player.PlayerColor).Select(x => x.CountryName))));
                    //int n = QueryTransferOfTroops(from.ArmySize - 1);
                    return (from, to, 1);
                }
            }
            else
            {
                Console.WriteLine("No more regrouping possible");
            }
            return (null, null, 0);


            throw new NotImplementedException();
        }
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
        protected override void Finish()
        {
            AnsiConsole.Render(new FigletText($"{Winner.PlayerColor} wins!").Color(Winner.PlayerColor.ToSpectreColor()));
            AnsiConsole.MarkupLine($"Objective:\n{ObjectiveManager.ObjectiveToString(Winner.PlayerObjective)} Accomplished!");
            Winner.Countries.ForEach(x => Console.WriteLine(x.CountryName));
            Environment.Exit(0);
        }
    }
}
