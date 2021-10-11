using Game.Common.ModelsDto;
using Refit;
using System.Threading.Tasks;
using System.Net.Http;

namespace GameClient.Common.ApiMethods
{
    public interface IApiMethods
    {
        [Post("/signin/check")]
        Task<ApiResponse<LoginResponseDto>> IsUserExist(LoginModelDto loginData);

        [Post("/signin/login")]
        Task<ApiResponse<LoginResponseDto>> Login(LoginModelDto loginData);

        [Post("/signin/registration")]
        Task<ApiResponse<HttpResponseMessage>> Registration(LoginModelDto loginData);

        [Get("/statistic/{login}")]
        Task<ApiResponse<StatisticDto>> GetStatistic(string login);
    }
}
