using System;
using System.Collections.Generic;
using System.Text;

namespace TEG
{
    static class DiceRoller
    {
        public static List<int> Roll(int n)
        {
            Random r = new Random();
            List<int> diceResults = new List<int>();
            for (int i = 0; i < n; i++)
            {
                diceResults.Add(r.Next(1, 7));
            }
            return diceResults;
        }
    }
}
