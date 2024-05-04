using Data.DBContext;
using Data.Models;
using XeDapAPI.Repository.Interface;

namespace XeDapAPI.Repository.Repositorys
{
    public class UserRepository : IUserInterface
    {
        private readonly MyDB _dbContext;
        public UserRepository(MyDB dbContext)
        {
            _dbContext = dbContext;
        }
        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public AccessToken GetValidTokenByUserId(int userId)
        {
            return _dbContext.AccessTokens.FirstOrDefault(x => x.Id == userId);
        }
    }
}
