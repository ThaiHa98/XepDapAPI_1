using Data.Models;

namespace XeDapAPI.Repository.Interface
{
    public interface IUserInterface
    {
        User GetUser(int id);
        AccessToken GetValidTokenByUserId(int userId);
    }
}
