using Blazored.SessionStorage;
using Game.Common.ModelsDto;
using GameClient.Common.Services.ApiServices;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameClient.Common.Pages
{
    [Route("statistic")]
    public partial class Statistic
    {
        [Inject] private IApiService _apiService { get; set; }
        [Inject] private ISessionStorageService _sessionStorageService { get; set; }
        private StatisticDto _statisticDto { get; set; } = new();
        private LoginModelDto _user { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
                _user = await _sessionStorageService.GetItemAsync<LoginModelDto>("User");

            _statisticDto = (await _apiService.ExecuteRequest(() => _apiService.ApiMethods.GetStatistic(_user.Login))).Data;
            StateHasChanged();
        }

    }
}
