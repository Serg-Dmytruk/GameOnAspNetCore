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
        public List<string> Games { get => games; }

        public GameService()
        {
            games = new List<string>();
        }

        public void CreateGame(string gameId)
        {
            games.Add(gameId);
        }

        public void DeleteGame(string gameId)
        {
            games.Remove(gameId);
        }

        public void JoinGame(string gameId)
        {
            games.Remove(gameId);
        }
    }
}
