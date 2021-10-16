using Game.Common.ModelsDto;
using GameClient.Common.Services.ApiServices;
using GameClient.Common.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.JSInterop;

namespace GameClient.Common.Pages
{
    [Route("/")]
    [Route("signin")]
    [Layout(typeof(EmptyLayout))]
    public partial class SignIn
    {
        [Inject] private IApiService _apiService { get; set; }
        [Inject] private NavigationManager _uriHelper { get; set; }
        [Inject] private ISessionStorageService _sessionStorageService { get; set; }
        [Inject] private IJSRuntime _jSRuntime { get; set; }
        private bool _waitingResponse { get; set; }
        private bool _showErrorMess { get; set; } = false;
        public LoginModelDto LoginData { get; set; } = new();
        private bool _rememberMe { get; set; } = false;
        private LoginResponseDto _loginRequest { get; set; } = new();
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                LoginData.Login = await _jSRuntime.InvokeAsync<string>("blazorExtensions.GetCookie", "login");
                LoginData.Password = await _jSRuntime.InvokeAsync<string>("blazorExtensions.GetCookie", "password");
                StateHasChanged();
            }
        }

        public async Task SigninRequest()
        {
            _waitingResponse = true;
            StateHasChanged();
            _loginRequest = (await _apiService.ExecuteRequest(() => _apiService.ApiMethods.Login(LoginData))).Data;
            _waitingResponse = false;

            if (!_loginRequest.UserFind)
                RedirectToRegistration();

            if (!_loginRequest.UserCanIn)
                _showErrorMess = true;

           

            if (_loginRequest.UserFind && _loginRequest.UserCanIn)
            {
                if (_rememberMe)
                    await _jSRuntime.InvokeAsync<string>("blazorExtensions.WriteCookie", LoginData.Login, LoginData.Password);

                StartSession();
                RedirectToHub();
            }

        }

        private void StartSession()
        {
            _sessionStorageService.SetItemAsync("User", LoginData);
        }

        private void RedirectToRegistration()
        {
            _uriHelper.NavigateTo("/registration");
        }

        private void RedirectToHub()
        {
            _uriHelper.NavigateTo("/hub");
        }
    }
}
