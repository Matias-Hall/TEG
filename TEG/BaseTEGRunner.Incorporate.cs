using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    partial class BaseTEGRunner
    {
        private void IncorporateTroops(Player player)
        {
            AddTroops(player, Math.Max(player.Countries.Count / 2, 3)); //Add troops equal to half of occupied countries.
            foreach (var continent in ContinentManager.Continents)
            {
                if (ContinentManager.PlayerInContinent(player, continent, continent.Size))
                {
                    ContinentBonus(player, continent);
                }
            }
        }
        private void ContinentBonus(Player player, Continent continent)
        {
            countryRenderer.RenderCountries(player, player.Countries.Where(x => x.Continent == continent).ToList());
            var countries = troopQuery.ChooseCountry(player, player.Countries.Where(x => x.Continent == continent).ToList(), continent.Bonus);
            if (countries.Values.ToList().Sum() != continent.Bonus)
            {
                throw new ArgumentOutOfRangeException();
            }
            foreach (var c in countries)
            {
                c.Key.AddTroops(c.Value);
            }
        }
    }
}
