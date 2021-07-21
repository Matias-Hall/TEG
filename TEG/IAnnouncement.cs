using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    public interface IAnnouncement
    {
        public void AttackResults(bool victory, List<int> attackingDice, List<int> defendingDice);
    }
}
