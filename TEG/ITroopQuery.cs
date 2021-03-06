using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    public interface ITroopQuery
    {
        public Dictionary<Country, int> ChooseCountry(Player player, List<Country> options, int troopsAvailable);
        public (bool regroup, Country from, Country to) ChooseFromToCountry(Player player, Dictionary<Country, List<Country>> options);
        public int QueryTransferOfTroops(int possibleTroops);
    }
}
