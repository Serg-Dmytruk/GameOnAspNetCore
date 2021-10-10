using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Game.Common.ModelsDto;
using GameClient.Common.ApiMethods;
using GameClient.Common.Services.ApiServices;
using GameClient.Common.Services;
using GameClient.Common.Services.HubServices;
using Blazored.SessionStorage;

namespace GameClient.Common.Pages
{
    [Route("hub")]
    public partial class GameHub
    {
        [Inject] private ISessionStorageService _sessionStorageService { get; set; }
        [Inject] HubService _hub { get; set; }
        
        public async Task TestConnect()
        {
            await _hub.TestConnect();
        }
    }
}
