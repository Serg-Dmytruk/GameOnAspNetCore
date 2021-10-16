using GameClient.Common.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace GameClient.Common.Services.HubServices
{
    
    public class CheckerHubService
    {
        private readonly IOptions<CheckersHubOption> _option;
        HubConnection _hubConnection;
        public CheckerHubService(IOptions<CheckersHubOption> option)
        {
            _option = option;
            _hubConnection = new HubConnectionBuilder().WithUrl(option.Value.Connection).Build();
        }
      
    }
}
