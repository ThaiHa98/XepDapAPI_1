using Data.Models;
using XeDapAPI.Dto;

namespace XeDapAPI.Service.Interfaces
{
    public interface IUserIService
    {
        public User RegisterUser(User user);
        User Login(RequestDto requetDto);
        bool logout (int UserId);
        string ResetPassword(int UserId);
    }
}
