using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Common.Services.TableService;

namespace GameServer.Hubs
{
    public class CheckerHub : Hub
    {
        private readonly ITableService _tableService;
        public const string HubUrl = "/checker-hub";

        public CheckerHub(ITableService tableService)
        {
            _tableService = tableService;
        }

        public async Task JoinTable(string tableId)
        {
            if ((await _tableService.GetAwailableTables()).Contains(tableId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                await Clients.GroupExcept(tableId, Context.ConnectionId).SendAsync("TableJoined");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                _tableService.AddTable(tableId, 1);
            }
        }
    }
}
