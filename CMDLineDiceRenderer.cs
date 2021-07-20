using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    static class CMDLineDiceRenderer
    {
        private static Dictionary<int, List<string>> DiceSections { get; set; }
        static CMDLineDiceRenderer()
        {
            Dictionary<int, List<string>> keyValuePairs = new Dictionary<int, List<string>>();
            keyValuePairs.Add(1, new List<string>
                {
                    "│         │",
                    "│         │",
                    "│    *    │",
                    "│         │",
                    "│         │"
                });
            keyValuePairs.Add(2, new List<string>
                {
                    "│       * │",
                    "│         │",
                    "│         │",
                    "│         │",
                    "│ *       │"
                });
            keyValuePairs.Add(3, new List<string>
                {
                    "│       * │",
                    "│         │",
                    "│    *    │",
                    "│         │",
                    "│ *       │"
                });
            keyValuePairs.Add(4, new List<string>
                {
                    "│ *     * │",
                    "│         │",
                    "│         │",
                    "│         │",
                    "│ *     * │"
                });
            keyValuePairs.Add(5, new List<string>
                {
                    "│ *     * │",
                    "│         │",
                    "│    *    │",
                    "│         │",
                    "│ *     * │"
                });
            keyValuePairs.Add(6, new List<string>
                {
                    "│ *     * │",
                    "│         │",
                    "│ *     * │",
                    "│         │",
                    "│ *     * │"
                });
            DiceSections = keyValuePairs;
        }
        public static string NumToDice(List<int> diceNums) //Gets a list of dice results (e.g. 1, 4, 3, 6) return string representations of physical dice on a line.
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < diceNums.Count; i++)
            {
                b.Append("┌─────────┐ ");
            }
            b.Append("\r\n");
            for (int i = 0; i < 5; i++)
            {
                foreach (int num in diceNums)
                {
                    b.Append(DiceSections[num][i]);
                    b.Append(' ');
                }
                b.Append("\r\n");
            }
            for (int i = 0; i < diceNums.Count; i++)
            {
                b.Append("└─────────┘ ");
            }
            return b.ToString();
        }
    }
}
