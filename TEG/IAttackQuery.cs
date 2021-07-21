using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    public interface IAttackQuery
    {
        public (bool attack, Country from, Country to) AttackFromTo(Player player, Dictionary<Country, List<Country>> options);
        public int QueryTransferOfTroops(int possibleTroops);
    }
}
