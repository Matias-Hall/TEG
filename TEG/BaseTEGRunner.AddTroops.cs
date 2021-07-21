using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    partial class BaseTEGRunner
    {
        private void AddTroops(Player player, int troopsAvailable)
        {
            countryRenderer.RenderCountries(player, player.Countries);
            var countries = troopQuery.ChooseCountry(player, player.Countries, troopsAvailable);
            if (countries.Values.ToList().Sum() != troopsAvailable)
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
