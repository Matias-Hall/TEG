using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEG;
using Spectre.Console;

namespace TEGCMDLineRender
{
    class CMDQueryAttack : IAttackQuery
    {
        public (bool attack, Country from, Country to) AttackFromTo(Player player, Dictionary<Country, List<Country>> options)
        {
            AnsiConsole.MarkupLine($"[{player.PlayerColor.ToSpectreColor()}]{player.PlayerColor}[/] attack!");
            if (AnsiConsole.Confirm("Attack?"))
            {
                Country attacking = CountryManager.CountryFromName(AnsiConsole.Prompt(new TextPrompt<string>("Country to attack from?").AddChoices(options.Keys.Select(x => x.CountryName)).HideChoices()));
                Country attacked = CountryManager.CountryFromName(AnsiConsole.Prompt(new TextPrompt<string>("Country to attack?").AddChoices(options[attacking].Select(x => x.CountryName)).HideChoices()));
                return (true, attacking, attacked);
            }
            else
            {
                return (false, null, null);
            }
        }
    }
}
