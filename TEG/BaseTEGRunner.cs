using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    public partial class BaseTEGRunner
    {
        private ICountryRender countryRenderer;
        private ITroopQuery troopQuery;
        private IAnnouncement announcements;
        private IAttackQuery attackQuery;
        public BaseTEGRunner(int playerNum)
        {
            CountryManager.LoadCountries();
            ContinentManager.LoadContinents();
            ObjectiveManager.LoadObjectives();
            CountryCardManager.LoadCountryCards();
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
        public void AddAttackQuery(IAttackQuery attackQuery)
        {
            this.attackQuery = attackQuery;
        }
        public void Run()
        {
            foreach (var player in PlayerManager.Players)
            {
                announcements.ShowObjective(player, player.PlayerObjective);
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
                Attack(player);
                RegroupTroops(player);
            }
            CountryManager.ResetReceivedTroops();
            while (true)
            {
                foreach (var player in PlayerManager.Players)
                {
                    IncorporateTroops(player);
                    Attack(player);
                    RegroupTroops(player);
                }
                CountryManager.ResetReceivedTroops();
            }
        }
    }
}
