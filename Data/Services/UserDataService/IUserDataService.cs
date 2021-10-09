using System.Threading.Tasks;

namespace Data.Services.UserDataService
{
    public interface IUserDataService
    {
        Task<bool> IsUserExist(string userLogin, string userPassword);
    }
}
