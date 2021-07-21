using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    public abstract partial class BaseTEGRunner
    {
        private ICountryRender countryRenderer;
        private ITroopQuery troopQuery;
        private IAnnouncement announcements;
        public BaseTEGRunner(int playerNum)
        {
            PlayerManager.LoadPlayers(playerNum);
        }
        public void AddCountryRenderer(ICountryRender countryR)
        {
            countryRenderer = countryR;
        }
        public void AddTroopQuery(ITroopQuery troopQ)
        {
            troopQuery = troopQ;
        }
        public void AddAnnouncements(IAnnouncement announcement)
        {
            announcements = announcement;
        }
        public void Run()
        {
            foreach (var player in PlayerManager.Players)
            {
                ShowObjective(player);
            }
            foreach (var player in PlayerManager.Players)
            {
                AddTroops(player, 5);

            }
            foreach (var player in PlayerManager.Players)
            {
                AddTroops(player, 3);
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
