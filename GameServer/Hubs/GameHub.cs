using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        public Task TestConnect()
        {
            return Clients.Caller.SendAsync("TestConnect", "Hello");
        }
    }
}
