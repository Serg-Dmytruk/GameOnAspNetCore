using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Common.ModelsDto;
using System.Net.Http;

namespace GameClient.Common.ApiMethods
{
    public interface  IApiMethods
    {
        [Post("/signin/check")]
        Task<ApiResponse<LoginResponseDto>> Login(LoginModelDto loginData);
    }
}
