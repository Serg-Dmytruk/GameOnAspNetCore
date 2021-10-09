using Game.Common.ModelsDto;
using System.Threading.Tasks;
using Data.Services.UserDataService;

namespace GameServer.Common.Services.SigninServices
{
    public class SigninService : ISigninService
    {
        private readonly IUserDataService _userDataService;
        public SigninService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public async Task<LoginResponseDto> IsUserExist(LoginModelDto request)
        {
            return new LoginResponseDto { UserFind = await _userDataService.IsUserExist(request.Login, request.Password) };
        }
        
    }
}
