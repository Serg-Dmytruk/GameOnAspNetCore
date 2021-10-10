using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        private List<string> games = new List<string>();
        public async Task TestConnect(string mess)
        {
            Debug.WriteLine(mess);
            await Clients.Caller.SendAsync("TestConnect", mess);
        }

        public async Task CreateGame(string gameId)
        {
            games.Add(gameId);
            Debug.WriteLine(gameId);
            await RefreshGame();
        }

        public async Task JoinGame(string gameId)
        {
            games.Remove(gameId);
            Debug.WriteLine(gameId);
            await RefreshGame();
        }

        private async Task RefreshGame()
        {
            await Clients.All.SendAsync("RefreshGame", games);
        }
    }
}
