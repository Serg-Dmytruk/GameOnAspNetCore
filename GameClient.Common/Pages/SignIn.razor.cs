using Game.Common.ModelsDto;
using GameClient.Common.Services.ApiServices;
using GameClient.Common.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Blazored.SessionStorage;

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
        private bool waitingResponse { get; set; }
        private bool showErrorMess { get; set; } = false;
        public LoginModelDto LoginData { get; set; } = new();
        private LoginResponseDto _loginRequest {get; set;} = new();
        public async Task SigninRequest()
        {
            waitingResponse = true;
            StateHasChanged();
            _loginRequest = (await _apiService.ExecuteRequest(() => _apiService.ApiMethods.Login(LoginData))).Data;
            waitingResponse = false;

            if (!_loginRequest.UserFind)
                RedirectToRegistration();

            if(!_loginRequest.UserCanIn)
                showErrorMess = true;

            if (_loginRequest.UserFind && _loginRequest.UserCanIn)
            {
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
