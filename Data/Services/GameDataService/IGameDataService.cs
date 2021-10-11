using Data.OutModels;
using System.Threading.Tasks;

namespace Data.Services.GameDataService
{
    public interface  IGameDataService
    {
       Task<StatisticOut> GetUserStatistic(string userLogin);
    }
}
