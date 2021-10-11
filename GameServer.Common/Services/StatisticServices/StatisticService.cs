using Data.Services.GameDataService;
using Game.Common.ModelsDto;
using System.Threading.Tasks;

namespace GameServer.Common.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IGameDataService _gameDataService;
        public StatisticService(IGameDataService gameDataService)
        {
            _gameDataService = gameDataService;
    }
        public async Task<StatisticDto> GetStatistic(string userLogin)
        {
            var userStatistic = await _gameDataService.GetUserStatistic(userLogin);
            return new StatisticDto
            {
                Score = userStatistic.Score,
                TotalGamesCount = userStatistic.TotalGamesCount,
                UserLogin = userStatistic.UserLogin,
                UserName = userStatistic.UserName
            };
        }
    }
}
