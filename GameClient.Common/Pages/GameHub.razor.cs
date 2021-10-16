using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Common.Services.HubServices;
using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using GameClient.Common.Shared;

namespace GameClient.Common.Pages
{
    [Route("hub")]
    [Layout(typeof(EmptyLayout))]
    public partial class GameHub
    {
        [Inject] HubService _hub { get; set; }
        public string Message => _hub.Message;
        public List<string> Games => _hub.Games;
        public string GameId => _hub.GameId;

        protected override async Task OnInitializedAsync()
        {
            _hub.Bind(this);
            await _hub.Connect();
        }

        public async Task TestConnect()
        {
            await _hub.TestConnect();
        }

        public async Task CreateGame()
        {
            await _hub.CreateGame();
        }

        public async Task DeleteGame()
        {
            await _hub.DeleteGame();
        }
                
        public async Task JoinGame(string gameId)
        {
            await _hub.JoinGame(gameId);
        }

        public async Task RefreshGame()
        {
            await _hub.RefreshGame();
            StateHasChanged();
        }

        public void Refresh()
        {
            StateHasChanged();
        }
    }
}
