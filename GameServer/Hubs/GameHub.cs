using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Common.Services.GameServices;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameService gameService;

        public GameHub(GameService gameService)
        {
            this.gameService = gameService;
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
            gameService.CreateGame(gameId);
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await RefreshGame();
        }

        public async Task DeleteGame(string gameId)
        {
            gameService.DeleteGame(gameId);
            await RefreshGame();
        }

        public async Task JoinGame(string gameId)
        {
            gameService.JoinGame(gameId); 
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await RefreshGame();
        }

        public async Task CallerRefreshGame()
        {
            await Clients.Caller.SendAsync("RefreshGame", gameService.Games);
        }

        public async Task RefreshGame()
        {
            await Clients.Others.SendAsync("RefreshGame", gameService.Games);
        }
    }
}
