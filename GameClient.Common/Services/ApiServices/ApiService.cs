using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Game.Common.ApiServices;
using GameClient.Common.ApiMethods;

namespace GameClient.Common.Services.ApiServices
{
    public class ApiService : DefaultApiService<IApiMethods>, IApiService
    {
        public ApiService(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration.GetConnectionString("Api")) { }
    }
}
