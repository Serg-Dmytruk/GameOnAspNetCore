using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace GameClient.Common.Services.HubServices
{
    public class HubService
    {
        private HubConnection connection;
        public string Message { get; private set; }
        public List<string> Games { get; private set; }
        public string GameId { get; private set; }
        public HubService()
        {
            Games = new List<string>();

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/gamehub")
                .Build();

            connection.On<string>("TestConnect", message => Message = $"state: {connection.State}, message: {message}");
            connection.On<List<string>>("RefreshGame", list => {
                Games = list;
                if (GameId != null)
                    Games.Remove(GameId);
                });

            connection.StartAsync();
        }

        public async Task TestConnect()
        {
            await connection.SendAsync("TestConnect", "Hello");
        }

        public async Task CreateGame()
        {
            GameId = Guid.NewGuid().ToString();
            await connection.SendAsync("CreateGame", GameId);
        }
        public async Task JoinGame(string gameId)
        {
            await connection.SendAsync("JoinGame", gameId);
        }
    }
}
