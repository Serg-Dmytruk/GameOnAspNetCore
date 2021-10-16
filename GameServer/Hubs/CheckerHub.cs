using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class CheckerHub : Hub
    {
        public const string HubUrl = "/checker-hub";

        public async Task JoinTable(string tableId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
            await Clients.GroupExcept(tableId, Context.ConnectionId).SendAsync("TableJoined");
        }
    }
}
