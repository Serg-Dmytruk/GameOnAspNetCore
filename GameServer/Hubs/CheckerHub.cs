using GameServer.Common.Services.TableService;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task JoinTable(string tableId, string login)
        {
            if ((await _tableService.GetAwailableTables()).Contains(tableId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                await Clients.GroupExcept(tableId, Context.ConnectionId).SendAsync("TableJoined", login);
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                _tableService.AddTable(tableId, 1);
            }
        }

        public async Task Move(string tableId, int prevCol, int prevRow, int newCol, int newRow)
        {
             await Clients.GroupExcept(tableId, Context.ConnectionId).SendAsync("Move", prevCol, prevRow, newCol, newRow);
           
        }
    }
}
