using System.Threading.Tasks;
using Game.Common.ModelsDto;

namespace GameServer.Common.Services.StatisticServices
{
    public interface IStatisticService
    {
        Task<StatisticDto> GetStatistic(string userLogin);
        Task UpdateStatistic(GameResultDto gameResultDto);
    }
}
