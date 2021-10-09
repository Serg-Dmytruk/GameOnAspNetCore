using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using Data.Models;

namespace Data.Services.UserDataService
{
    public class UserDataService : IUserDataService
    {
        private readonly AppDbContext _appDbContext;
        public UserDataService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> IsUserExist(string userLogin)
        {
            return await _appDbContext.UserDatas.AnyAsync(u => u.Login == userLogin);
        }

        public async Task<bool> Login(string userLogin, string userPassword)
        {
            var user = await _appDbContext.UserDatas.FirstOrDefaultAsync(u => u.Login == userLogin);

            if (user == null || !BC.Verify(userPassword, user.Password))
                return false;

            return true;
        }

        public async Task SaveUser(string login, string password, string userName)
        {
            await _appDbContext.AddAsync(new UserData {
                Login = login,
                Password = BC.HashPassword(password),
                UserName = userName,
                UserInfo = new GameData { Score = 0, TotalGamesCount = 0}
            });

            await _appDbContext.SaveChangesAsync();
        }
    }
}
