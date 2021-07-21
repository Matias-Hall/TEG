using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    abstract partial class BaseTEGRunner
    {
        protected abstract (Country attackingCountry, Country defendingCountry) Attack(Player player); //Choose what country to attack; Return country id to attack and what country is attacking it.
        private void InternalAttack(Player player) //Calls the Attack method, processes the attack, updates countries, calls AttackResults method, and asks for troop transfer if necessary.
        {
            var (attacking, defending) = Attack(player);
            while (attacking != null)
            {
                List<int> attackingDice = DiceRoller.Roll(Math.Min(3, attacking.ArmySize - 1)); //Only 3 dice at most.
                List<int> defendingDice = DiceRoller.Roll(Math.Min(3, defending.ArmySize)); //Only 3 dice at most.
                attackingDice = attackingDice.OrderByDescending(f => f).ToList();
                defendingDice = defendingDice.OrderByDescending(f => f).ToList();
                for (int i = 0; i < Math.Min(attackingDice.Count, defendingDice.Count); i++)
                {
                    if (attackingDice[i] > defendingDice[i])
                    {
                        defending.AddTroops(-1);
                    }
                    else //If the die of the defending country is bigger OR equal to the corresponding attacking die remove from attacking country.
                    {
                        attacking.AddTroops(-1);
                    }
                }
                bool victory = defending.ArmySize < 1;
                AttackResults(victory, attackingDice, defendingDice);
                if (victory)
                {
                    Player prevOwner = PlayerManager.PlayerFromColor(defending.ControllingColor);
                    defending.ControllingColor = player.PlayerColor;
                    int transfer = QueryTransferOfTroops(attacking.ArmySize - 1);
                    defending.AddTroops(transfer);
                    attacking.AddTroops(-transfer);
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
                        Console.Beep();
                    }
                }
                (attacking, defending) = Attack(player);
            }
        }
        protected abstract void AttackResults(bool victory, List<int> attackingDice, List<int> defendingDice); //Feeds results of battle to render, either by command line or any other way.
        protected abstract int QueryTransferOfTroops(int possibleTroops); //Asks for number of troops to transfer to a newly conquered country.
        private void TempAttack(Player player)
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
            
        }
    }
}
