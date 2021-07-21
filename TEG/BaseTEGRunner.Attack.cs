using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    abstract partial class BaseTEGRunner
    {
        private void Attack(Player player)
        {
            bool continueAttacking = true;
            Country from;
            Country to;
            while (continueAttacking)
            {
                var options = new Dictionary<Country, List<Country>>();
                foreach (Country c in player.Countries)
                {
                    if (c.ArmySize > 1)
                    {
                        List<Country> countryToAttack = (from k in c.Neighbors
                                                         where k.ControllingColor != player.PlayerColor
                                                         select k).ToList();
                        if (countryToAttack.Count > 0)
                        {
                            options.Add(c, countryToAttack);
                        }
                    }
                }
                countryRenderer.RenderFromToCountries(player, options);
                if (options.Count > 0)
                {
                    (continueAttacking, from, to) = attackQuery.AttackFromTo(player, options);
                    if (continueAttacking)
                    {
                        List<int> attackingDice = DiceRoller.Roll(Math.Min(3, from.ArmySize - 1)); //Only 3 dice at most.
                        List<int> defendingDice = DiceRoller.Roll(Math.Min(3, to.ArmySize)); //Only 3 dice at most.
                        attackingDice = attackingDice.OrderByDescending(f => f).ToList();
                        defendingDice = defendingDice.OrderByDescending(f => f).ToList();
                        for (int i = 0; i < Math.Min(attackingDice.Count, defendingDice.Count); i++)
                        {
                            if (attackingDice[i] > defendingDice[i])
                            {
                                to.AddTroops(-1);
                            }
                            else //If the die of the defending country is bigger OR equal to the corresponding attacking die remove from attacking country.
                            {
                                from.AddTroops(-1);
                            }
                        }
                        bool victory = to.ArmySize < 1;
                        announcements.AttackResults(victory, attackingDice, defendingDice);
                        if (victory)
                        {
                            Player prevOwner = PlayerManager.PlayerFromColor(to.ControllingColor);
                            to.ControllingColor = player.PlayerColor;
                            int transfer = attackQuery.QueryTransferOfTroops(from.ArmySize - 1);
                            to.AddTroops(transfer);
                            from.AddTroops(-transfer);
                            bool wonGame = false;
                            if (prevOwner.Countries.Count == 0)
                            {
                                wonGame = player.PlayerObjective.ObjectiveAccomplished(player);
                                prevOwner.IsDestroyed = true;
                            }
                            else
                            {
                                wonGame = player.PlayerObjective.ObjectiveAccomplished(player);
                            }
                            if (wonGame)
                            {
                                Winner = player;
                                Finish();
                            }
                        }
                    }
                }
            }
        }
    }
}
