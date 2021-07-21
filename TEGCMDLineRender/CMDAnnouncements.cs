using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG;
using Spectre.Console;

namespace TEGCMDLineRender
{
    class CMDAnnouncements : IAnnouncement
    {
        public void AttackResults(bool victory, List<int> attackingDice, List<int> defendingDice)
        {
            Console.WriteLine(CMDLineDiceRenderer.NumToDice(attackingDice));
            AnsiConsole.Render(new FigletText("VS"));
            Console.WriteLine(CMDLineDiceRenderer.NumToDice(defendingDice));
            if (victory) AnsiConsole.MarkupLine($"Country [purple]conquered[/]");
            else AnsiConsole.MarkupLine($"Country remains [purple]free[/]");
            Console.ReadKey(true);
        }
        public void ShowObjective(Player player, Objective objective)
        {
            AnsiConsole.MarkupLine($"[{player.PlayerColor.ToSpectreColor()}]{player.PlayerColor}[/] objective:");
            Console.WriteLine(ObjectiveManager.ObjectiveToString(objective));
            Console.Write("Delete secret");
            Console.ReadKey(true);
            Console.Clear();
        }
        public void Finish(Player player)
        {
            AnsiConsole.Render(new FigletText($"{player.PlayerColor} wins!").Color(player.PlayerColor.ToSpectreColor()));
            AnsiConsole.MarkupLine($"Objective:\n{ObjectiveManager.ObjectiveToString(player.PlayerObjective)} Accomplished!");
            player.Countries.ForEach(x => Console.WriteLine(x.CountryName));
            Environment.Exit(0);
        }
    }
}
