using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    abstract partial class BaseTEGRunner
    {
        private void InternalIncorporateTroops(Player player)
        {
            AddTroops(player, Math.Max(player.Countries.Count / 2, 3)); //Add troops equal to half of occupied countries.
            foreach (var continent in ContinentManager.Continents)
            {
                if (ContinentManager.PlayerInContinent(player, continent, continent.Size))
                {
                    //InternalAddTroops(ContinentBonus(player, continent, continent.Bonus));
                }
            }
        }
        protected abstract Dictionary<Country, int> ContinentBonus(Player player, Continent continent, int troopsAvailable);

    }
}
