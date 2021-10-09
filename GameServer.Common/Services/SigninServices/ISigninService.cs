using Game.Common.ModelsDto;
using System.Threading.Tasks;

namespace GameServer.Common.Services.SigninServices
{
    public interface ISigninService
    {
        Task<LoginResponseDto> IsUserExist(LoginModelDto request);
    }
}
