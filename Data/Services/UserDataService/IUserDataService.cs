using System.Threading.Tasks;

namespace Data.Services.UserDataService
{
    public interface IUserDataService
    {
        Task<bool> IsUserExist(string userLogin);
        Task<bool> Login(string userLogin, string userPassword);
        Task SaveUser(string login, string password, string userName);
    }
}
