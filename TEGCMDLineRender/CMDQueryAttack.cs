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
