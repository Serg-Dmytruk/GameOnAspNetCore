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

namespace GameClient.Common.Pages
{
    [Microsoft.AspNetCore.Components.Route("hub")]
    public partial class GameHub
    {
        [Inject] HubService _hub { get; set; }
        
        public string Message => _hub.Message;
        public List<string> Games => _hub.Games;
        public string GameId => _hub.GameId;

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
    }
}
