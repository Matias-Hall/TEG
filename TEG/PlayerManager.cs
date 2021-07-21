using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    static class PlayerManager
    {
        public static List<Player> Players { get => (from p in allPlayers where !p.IsDestroyed select p).ToList(); } //Only players that haven't lost are given.
        private static List<Player> allPlayers; //Holds all players, even ones that have lost.
        public static void LoadPlayers(int playerNum)
        {
            List<Player> players = new List<Player>();
            List<int> nums = Enumerable.Range(1, ObjectiveManager.ObjNum - 1).ToList().Shuffle().ToList(); //Starts from 1 since 0 is the common objective.
            for (int i = 0; i < playerNum; i++)
            {
                players.Add(new Player((TEGColor)i, new Objective(nums[i])));
            }
            allPlayers = CountryManager.SortCountries(players);
        }
        public static Player? PlayerFromColor(TEGColor color)
        {
            return allPlayers.Find(x => x.PlayerColor == color);
        }
    }
}
