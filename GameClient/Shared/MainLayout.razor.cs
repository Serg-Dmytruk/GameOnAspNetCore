using System.Threading.Tasks;
using Blazored.SessionStorage;
using Game.Common.ModelsDto;
using Microsoft.AspNetCore.Components;


namespace GameClient.Shared
{
    public partial class MainLayout
    {
        [Inject] private NavigationManager _uriHelper { get; set; }
        [Inject] private ISessionStorageService _sessionStorageService { get; set; }
        private LoginModelDto _user{ get; set; }
  
        private async Task GetSesionUser()
        {
            _user =  await _sessionStorageService.GetItemAsync<LoginModelDto>("User");
            if (_user == null)
                RedirectToLogin();
        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
             await GetSesionUser();

            StateHasChanged();
        }
        private void RedirectToLogin()
        {
            _uriHelper.NavigateTo($"/signin");
        }

        //private void Logout()
        //{
        //    _sessionStorageService.ClearAsync();
        //    RedirectToLogin();
        //}
    }

}
