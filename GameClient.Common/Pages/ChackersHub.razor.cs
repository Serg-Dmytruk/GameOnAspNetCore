using GameClient.Common.Options;
using GameClient.Common.Services.HubServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Common.Pages
{
    [Route("chackers-hub")]
    public partial class ChackersHub
    {
        [Inject] private CheckerHubService _checkerHubService { get; set; }
        private bool _inGame { get; set; } = false;

        private List<string> _tables = new List<string>();

        private async Task CreateGame()
        {
            await _checkerHubService.HubConnection.StartAsync();
            string tableId = Guid.NewGuid().ToString();
            await _checkerHubService.HubConnection.SendAsync("JoinTable", tableId);
            _inGame = true;
        }

        private async Task JoinGame(string tableId)
        {
            await _checkerHubService.HubConnection.StartAsync();
            await _checkerHubService.HubConnection.SendAsync("JoinTable", tableId);
            _inGame = true;
        }

        private async Task RefreshTables()
        {

        }

    }
}
