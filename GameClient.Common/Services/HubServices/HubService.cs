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

        public HubService()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/gamehub")
                .Build();

            connection.On<string>("TestConnect", mess => Debug.WriteLine(mess));

            connection.StartAsync();
            Debug.WriteLine(connection.State);
        }

        public async Task TestConnect()
        {
            Debug.WriteLine(connection.State);
            await connection.SendAsync("TestConnect", "hello");
        }
    }
}
