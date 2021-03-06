using Game.Common.ModelsDto;
using Refit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

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

        [Post("/statistic/update")]
        Task<ApiResponse<HttpResponseMessage>> UpdateStatistic(GameResultDto result);

        [Get("/statistic/{login}")]
        Task<ApiResponse<StatisticDto>> GetStatistic(string login);

        [Get("/ckeckers/tables")]
        Task<ApiResponse<List<string>>> GetTables();
    }
}
