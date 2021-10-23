using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Common.Services.GameServices
{
    public class GameService
    {
        private List<string> games;
        private List<string> runingGames;
        public List<string> Games { get => games; }
        public List<string> RuningGames { get => runingGames; }
        public GameService()
        {
            games = new List<string>();
            runingGames = new List<string>();
        }

        public void CreateGame(string gameId)
        {
            games.Add(gameId);
        }

        public void DeleteGame(string gameId)
        {
            games.Remove(gameId);
        }

        public void StartGame(string gameId)
        {
            games.Remove(gameId);
            runingGames.Add(gameId);
        }
    }
}
