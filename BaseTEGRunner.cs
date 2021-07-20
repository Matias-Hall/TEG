using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    abstract partial class BaseTEGRunner
    {
        public BaseTEGRunner(int playerNum)
        {
            PlayerManager.LoadPlayers(playerNum);
        }
        public void Run()
        {
            foreach (var player in PlayerManager.Players)
            {
                ShowObjective(player);
            }
            foreach (var player in PlayerManager.Players)
            {
                InternalAddTroops(AddTroops(player, 5));

            }
            foreach (var player in PlayerManager.Players)
            {
                InternalAddTroops(AddTroops(player, 3));
            }
            foreach (var player in PlayerManager.Players)
            {
                InternalAttack(player);
                InternalRegroupTroops(player);
            }
            while (Winner == null)
            {
                foreach (var player in PlayerManager.Players)
                {
                    InternalIncorporateTroops(player);
                    InternalAttack(player);
                    InternalRegroupTroops(player);
                }
            }
        }
    }
}
