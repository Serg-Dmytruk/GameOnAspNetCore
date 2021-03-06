using Blazored.SessionStorage;
using Game.Common.ModelsDto;
using GameClient.Common.Services.ApiServices;
using GameClient.Common.Services.HubServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameClient.Common.Pages
{
    [Route("chackers-hub")]
    public partial class ChackersHub
    {
        [Inject] private CheckerHubService _checkerHubService { get; set; }
        [Inject] private IApiService _apiService { get; set; }
        [Inject] private ISessionStorageService _sessionStorageService { get; set; }
        private bool _inGame { get; set; } = false;
        private string _tableId { get; set; }
        private List<string> _tables = new List<string>();
        private bool _isWhite { get; set; }

        protected override async Task OnInitializedAsync()
        {
           await RefreshTables();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
                await _checkerHubService.HubConnection.StartAsync();
        }

        private async Task CreateGame()
        {
            // await _checkerHubService.HubConnection.StartAsync();
            _tableId = Guid.NewGuid().ToString();
            await _checkerHubService.HubConnection.SendAsync("JoinTable", _tableId, "");
            _isWhite = true;
            _inGame = true;
        }

        private async Task JoinGame(string tableId)
        {
            string login = (await _sessionStorageService.GetItemAsync<LoginModelDto>("User")).Login;
           // await _checkerHubService.HubConnection.StartAsync();
            _tableId = tableId;
            await _checkerHubService.HubConnection.SendAsync("JoinTable", tableId, login);
            _isWhite = false;
            _inGame = true;
            
        }

        private async Task RefreshTables()
        {
            _tables = (await _apiService.ExecuteRequest(() => _apiService.ApiMethods.GetTables())).Data;
        }

    }
}
