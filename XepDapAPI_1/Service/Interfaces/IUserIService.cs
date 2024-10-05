using Data.Dto;
using Data.Models;
using XeDapAPI.Dto;

namespace XeDapAPI.Service.Interfaces
{
    public interface IUserIService
    {
        public UserDto RegisterUser(UserDto userdto);
        public UserDto RegisterUserAdmin(UserDto userdto);
        User Login(RequestDto requetDto);
        bool logout (int UserId);
        string ResetPassword(int UserId);
    }
}
