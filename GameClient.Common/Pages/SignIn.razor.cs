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

namespace GameClient.Common.Pages
{
    [Route("/")]
    [Route("signin")]
    [Layout(typeof(EmptyLayout))]
    public partial class SignIn
    {
        [Inject] private IApiService _apiService { get; set; }
        public LoginModelDto LoginData { get; set; } = new();

        public async Task SigninRequest()
        {
            var result = await _apiService.ExecuteRequest(() => _apiService.ApiMethods.Login(LoginData));
        }
    }
}
