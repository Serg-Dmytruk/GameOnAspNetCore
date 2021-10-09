using Game.Common.ModelsDto;
using System.Threading.Tasks;
using Data.Services.UserDataService;
using GameServer.Common.Option;
using Microsoft.Extensions.Options;

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
            return new LoginResponseDto { UserFind = await _userDataService.IsUserExist(request.Login) };
        }

        public async Task<LoginResponseDto> Login(LoginModelDto request)
        {
            return new LoginResponseDto
            {
                UserFind = await _userDataService.IsUserExist(request.Login),
                UserCanIn = await _userDataService.Login(request.Login, request.Password)
            };
        }

        public async Task AddUser(LoginModelDto request)
        {
            await _userDataService.SaveUser(request.Login, request.Password, request.UserName);
        }

    }
}
