using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using GameClient.Common.Shared;
using Game.Common.ModelsDto;
using GameClient.Common.ApiMethods;
using GameClient.Common.Services.ApiServices;
using GameClient.Common.Services;
using GameClient.Common.Services.HubServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace GameClient.Common.Pages
{
    [Route("hub")]
    public partial class GameHub
    {
        [Inject] HubService _hub { get; set; }
        
        public string Message => _hub.Message;
        public List<string> Games => _hub.Games;
        public string GameId => _hub.GameId;

        protected override async Task OnInitializedAsync()
        {
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
                
        public async Task JoinGame(string gameId)
        {
            await _hub.JoinGame(gameId);
        }

        public async Task Refresh()
        {
            await _hub.RefreshGame();
            StateHasChanged();
        }
    }
}
