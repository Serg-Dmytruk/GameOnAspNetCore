using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Common.Pages;
using Microsoft.AspNetCore.SignalR.Client;

namespace GameClient.Common.Services.HubServices
{
    public class HubService
    {
        private HubConnection connection;
        public string Message { get; private set; }
        public List<string> Games { get; private set; }
        public string GameId { get; private set; }
        public bool IsRun { get; private set; }
        public HubService()
        {
            Games = new List<string>();

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/gamehub")
                .Build();
            IsRun = false;
        }

        public void Bind(GameHub gameHub)
        {
            connection.On<string>("TestConnect", message =>
                {
                    Message = $"state: {connection.State}, message: {message}";
                    gameHub.Refresh();    
                });
            connection.On<List<string>>("RefreshGame", list =>
                {
                    list.Remove(GameId);
                    Games = list;
                    gameHub.Refresh();
                });
            connection.On<bool>("StartGame", state =>
                {
                    IsRun = state;
                    gameHub.Refresh();
                });
            connection.StartAsync();
        }

        public async Task TestConnect()
        {
            await connection.SendAsync("TestConnect", "Hello");
        }

        public async Task Connect()
        {
            await connection.SendAsync("Connect", "UserId");
        }

        public async Task CreateGame()
        {
            GameId = Guid.NewGuid().ToString();
            await connection.SendAsync("CreateGame", GameId);
        }

        public async Task DeleteGame()
        {
            await connection.SendAsync("DeleteGame", GameId);
            GameId = null;
        }
        public async Task JoinGame(string gameId)
        {
            await connection.SendAsync("JoinGame", gameId);
            GameId = gameId;
        }

        public async Task RefreshGame()
        {
            await connection.SendAsync("CallerRefreshGame");
        }
    }
}
