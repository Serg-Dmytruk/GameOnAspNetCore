using GameClient.Common.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace GameClient.Common.Services.HubServices
{

    public class CheckerHubService
    {
        public HubConnection HubConnection { get; set; }
        public CheckerHubService(IOptions<CheckersHubOption> option)
        {
            HubConnection = new HubConnectionBuilder().WithUrl(option.Value.Connection).Build();
        }
      
    }
}
