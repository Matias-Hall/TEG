using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    abstract partial class BaseTEGRunner
    {
        protected abstract (Country from, Country to, int troopNumber) RegroupTroops(Player player); //Asks for troops to move from one country to another.
        private void InternalRegroupTroops(Player player)
        {
            (Country from, Country to, int troopNumber) = RegroupTroops(player);
            while (from != null)
            {
                from.AddTroops(-troopNumber);
                to.AddTroops(troopNumber);
                to.ReceivedTroops = true;
                (from, to, troopNumber) = RegroupTroops(player);
            }

        }
    }
}