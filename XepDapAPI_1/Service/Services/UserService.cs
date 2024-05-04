using Data.DBContext;
using Data.Models;
using Data.Models.Enum;
using XeDapAPI.Dto;
using XeDapAPI.Helper;
using XeDapAPI.Repository.Interface;
using XeDapAPI.Service.Interfaces;

namespace XeDapAPI.Service.Services
{
    public class UserService : IUserIService
    {
        private readonly MyDB _DbContex;
        private readonly Token _token;
        private readonly IUserInterface _userInterface;
        public UserService(MyDB dbContex, Token token, IUserInterface userInterface)
        {
            _userInterface = userInterface;
            _token = token;
            _DbContex = dbContex;
        }
        public User Login(RequestDto requetDto)
        {
            try
            {
                var user = _DbContex.Users.FirstOrDefault(x => x.Email == requetDto.Email);

                if (user == null)
                {
                    throw new Exception("Email not found!");
                }

                if (!BCrypt.Net.BCrypt.Verify(requetDto.Password, user.Password))
                {
                    throw new Exception("Incorrect password!");
                }

                UpdateOrCreateAccessToken(user);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while logging in.", ex);
            }
        }

        public bool logout(int UserId)
        {
            try
            {
                var userToken = _userInterface.GetValidTokenByUserId(UserId);
                if(userToken != null)
                {
                    var tokenValue = userToken.AcessToken;
                    var principal = _token.ValidataToken(tokenValue);
                    if (principal != null)
                    {
                        userToken.Status = StatusToken.Expired;
                    }
                }
                _DbContex.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during logout for user {UserId}: {ex.Message}");
                return false;
            }
        }

        public User RegisterUser(User user)
        {
            try
            {
                if(user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User object is null or missing required information.");
                }
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _DbContex.Users.Add(user);
                _DbContex.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error when creating a User",ex);
            }
        }

        public string ResetPassword(int UserId)
        {
            throw new NotImplementedException();
        }
        public void UpdateOrCreateAccessToken(User user)
        {
            var existingToken = _userInterface.GetValidTokenByUserId(user.Id);
            if(existingToken != null)
            {
                var token = _token.CreateToken(user);
                if (string.IsNullOrEmpty(token))
                    throw new Exception("Failed to create a token.");
                existingToken.AcessToken = token;
                existingToken.ExpirationDate = DateTime.Now;
            }
            else
            {
                var token = _token.CreateToken(user);
                if (string.IsNullOrEmpty(token))
                    throw new Exception("Failed to create a token.");
                var accessToken = new AccessToken
                {
                    UserId = user.Id,
                    AcessToken = token,
                    Status = StatusToken.Valid,
                    ExpirationDate = DateTime.Now,
                };
                _DbContex.AccessTokens.Add(accessToken);
            }
            _DbContex.SaveChanges();
        }
    }
}
