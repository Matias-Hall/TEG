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
    }
}
