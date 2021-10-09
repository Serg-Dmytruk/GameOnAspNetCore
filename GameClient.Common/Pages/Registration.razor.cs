using Game.Common.ModelsDto;
using GameClient.Common.Services.ApiServices;
using GameClient.Common.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net;
using System;

namespace GameClient.Common.Pages
{
    [Route("registration")]
    [Layout(typeof(EmptyLayout))]
    public partial class Registration
    {
        [Inject] private IApiService _apiService { get; set; }
        [Inject] private NavigationManager _uriHelper { get; set; }
        public LoginModelDto LoginData { get; set; } = new();

        private bool validate { get; set; } = true;
        private bool validateL { get; set; } = true;
        private bool validateP { get; set; } = true;
        private bool validateU { get; set; } = true;
        private string errorMessage { get; set; } = string.Empty;
        private bool waitingResponse { get; set; } = false;
        private async Task RegisterUser()
        {
            waitingResponse = true;
            StateHasChanged();

            errorMessage = string.Empty;

            validateL = !(await _apiService.ExecuteRequest(() => _apiService.ApiMethods.IsUserExist(LoginData))).Data.UserFind;
            if (string.IsNullOrEmpty(LoginData.Login))
                validateL = false;
            if (!validateL)
                errorMessage += $"login alredy exist or empty {Environment.NewLine}";

            validateP = IsPasswordSame();
            if (!validateP)
                errorMessage += $"passwords are not same or empty {Environment.NewLine}";

            validateU = !string.IsNullOrEmpty(LoginData.UserName);
            if (!validateU)
                errorMessage += $"empty user name {Environment.NewLine}";

            SetValidate();

            if (validate)
            {
                var response = await _apiService.ExecuteRequest(() => _apiService.ApiMethods.Registration(LoginData));
                if (response.StatusCode == HttpStatusCode.OK)
                    RedirectToSignIn();
                else
                    errorMessage += $"server error {Environment.NewLine}";
            }

            waitingResponse = false;
        }

        private void RedirectToSignIn()
        {
            _uriHelper.NavigateTo("/signin");
        }

        private void SetValidate()
        {
            if (validateL && validateP && validateU)
                validate = true;
            else
                validate = false;
        }
        public bool IsPasswordSame()
        {
            if (string.IsNullOrEmpty(LoginData.Password))
                return false;

            if (LoginData.Password == LoginData.PasswordCopy)
                return true;

            return false;
        }
    }

}
