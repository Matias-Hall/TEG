using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    partial class BaseTEGRunner
    {
        private void RegroupTroops(Player player)
        {
            bool continueRegrouping = true;
            Country from, to;
            while (continueRegrouping)
            {
                var options = new Dictionary<Country, List<Country>>();
                foreach (Country c in player.Countries)
                {
                    if (c.ArmySize > 1)
                    {
                        List<Country> countryToAttack = (from k in c.Neighbors
                                                         where k.ControllingColor == player.PlayerColor && !k.ReceivedTroops
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
                    (continueRegrouping, from, to) = troopQuery.ChooseFromToCountry(player, options);
                    if (continueRegrouping)
                    {
                        int n = troopQuery.QueryTransferOfTroops(from.ArmySize - 1);
                        from.AddTroops(-n);
                        to.AddTroops(n);
                        to.ReceivedTroops = true;
                    }
                }
            }
        }
    }
}