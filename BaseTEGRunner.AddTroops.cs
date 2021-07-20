using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    abstract partial class BaseTEGRunner
    {
        protected abstract Dictionary<Country, int> AddTroops(Player player, int troopsAvailable);
        private void InternalAddTroops(Dictionary<Country, int> countries) //Assigns the troops to countries in one player.
        {
            foreach (var c in countries)
            {
                c.Key.AddTroops(c.Value); //Finds the country in the player's country list by name and adds the specified amount of troops.
            }
        }
    }
}
