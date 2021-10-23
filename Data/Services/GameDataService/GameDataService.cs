using Data.Context;
using Data.OutModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Services.GameDataService
{
    public class GameDataService : IGameDataService
    {
        private readonly AppDbContext _appDbContext;
        public GameDataService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<StatisticOut> GetUserStatistic(string userLogin)
        {
            return await _appDbContext.UserDatas.Include(u => u.UserInfo).Where(u => u.Login == userLogin).Select(x => new StatisticOut
            {
                Score = x.UserInfo.Score,
                UserLogin = x.Login,
                UserName = x.UserName,
                TotalGamesCount = x.UserInfo.TotalGamesCount
            }).FirstOrDefaultAsync();
        }

        public async Task UpdateStatistic(string userLogin, bool isWin)
        {
            var user = await _appDbContext.UserDatas.Include(u => u.UserInfo).FirstOrDefaultAsync(x => x.Login == userLogin);

            if (user == null)
                return;

            var userInfo = user.UserInfo;
            userInfo.TotalGamesCount ++;

            if (isWin)
                userInfo.Score++;

            _appDbContext.GameDatas.Update(userInfo);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
