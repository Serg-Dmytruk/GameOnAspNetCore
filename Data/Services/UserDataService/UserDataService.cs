using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Services.UserDataService
{
    public class UserDataService : IUserDataService
    {
        private readonly AppDbContext _appDbContext;
        public UserDataService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> IsUserExist(string userLogin, string userPassword)
        {
            return await _appDbContext.UserDatas.AnyAsync(u => u.Login == userLogin && u.Password == userPassword);
        }
    }
}
