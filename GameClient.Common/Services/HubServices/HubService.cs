using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace GameClient.Common.Services.HubServices
{
    class HubService
    {
        private HubConnection connection;

        public HubService()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:60667/gamehub")
                .Build();
            connection.StartAsync();

            connection.On<string>("TestConnect", mess => Console.WriteLine(mess));
        }

        public async Task TestConnect()
        {
            await connection.SendAsync("TestConnect", "hello");
        }
    }
}
