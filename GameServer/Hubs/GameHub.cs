using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        private List<string> games;
        public GameHub()
        {
            games = new List<string>();
        }
        public async Task TestConnect(string mess)
        {
            await Clients.Caller.SendAsync("TestConnect", mess);
        }

        public async Task Connect(string mess)
        {
            await CallerRefreshGame();
        }

        public async Task CreateGame(string gameId)
        {
            games.Add(gameId);
            Debug.WriteLine(gameId);
            await RefreshGame();
        }

        public async Task JoinGame(string gameId)
        {
            //games.Remove(gameId);
            Debug.WriteLine(gameId);
            await RefreshGame();
        }

        public async Task CallerRefreshGame()
        {
            Debug.WriteLine($"[CallerRefreshGame] {games.Count}");
            await Clients.Caller.SendAsync("RefreshGame", games);
        }

        public async Task RefreshGame()
        {
            Debug.WriteLine($"[RefreshGame] {games.Count}");
            await Clients.Others.SendAsync("RefreshGame", games);
        }
    }
}
