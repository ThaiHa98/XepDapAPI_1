using Data.DBContext;
using Data.Dto;
using Data.Models;
using Data.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public UserService(MyDB dbContex, Token token, IUserInterface userInterface, IConfiguration configuration)
        {
            _userInterface = userInterface;
            _token = token;
            _DbContex = dbContex;
            _configuration = configuration;
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

        public UserDto RegisterUser(UserDto userdto)
        {
            try
            {
                if (userdto == null)
                {
                    throw new ArgumentNullException(nameof(userdto), "User object is null or missing required information.");
                }
                var user = new User
                {
                    Name = userdto.Name,
                    Email = userdto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userdto.Password),
                    Address = userdto.Address,
                    City = userdto.City,
                    Phone = userdto.Phone,
                    DateOfBirth = userdto.DateOfBirth,
                };
                user.roles = Roles.User;
                user.Create = DateTime.Now;
                _DbContex.Users.Add(user);
                _DbContex.SaveChanges();
                return userdto;
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error when creating a User", ex);
            }
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            try
            {
                string currentDataFolder = DateTime.Now.ToString("dd-MM-yyyy");
                var baseFolder = _configuration.GetValue<string>("BaseAddress");

                // Tạo thư mục Product
                var productFolder = Path.Combine(baseFolder, "User");

                if (!Directory.Exists(productFolder))
                {
                    Directory.CreateDirectory(productFolder);
                }

                // Tạo thư mục date nằm trong thư mục Product
                var folderPath = Path.Combine(productFolder, currentDataFolder);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream); // Lưu hình ảnh vào file
                }

                // Trả về tên thư mục và tên ảnh
                return Path.Combine("Product", currentDataFolder, fileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving the image: {ex.Message}");
            }
        }

        public UserDto RegisterUserAdmin(UserDto userdto)
        {
            try
            {
                if (userdto == null)
                {
                    throw new ArgumentNullException(nameof(userdto), "User object is null or missing required information.");
                }
                var user = new User
                {
                    Name = userdto.Name,
                    Email = userdto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userdto.Password),
                    Address = userdto.Address,
                    City = userdto.City,
                    Phone = userdto.Phone,
                    DateOfBirth = userdto.DateOfBirth,
                };
                user.roles = Roles.ManagerMent;
                user.Create = DateTime.Now;
                _DbContex.Users.Add(user);
                _DbContex.SaveChanges();
                return userdto;
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error when creating a User", ex);
            }
        }

        private async Task<string> SaveImageAsyncAdmin(IFormFile image)
        {
            try
            {
                string currentDataFolder = DateTime.Now.ToString("dd-MM-yyyy");
                var baseFolder = _configuration.GetValue<string>("BaseAddress");
                var productFolder = Path.Combine(baseFolder, "UserAdmin");

                if (!Directory.Exists(productFolder))
                {
                    Directory.CreateDirectory(productFolder);
                }
                var folderPath = Path.Combine(productFolder, currentDataFolder);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                return Path.Combine("Product", currentDataFolder, fileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving the image: {ex.Message}");
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

        public async Task<GetViewUser> GetUser(int UserId)
        {
            try
            {
                var userEntity = await _DbContex.Users.FirstOrDefaultAsync(x => x.Id == UserId);
                if (userEntity == null)
                {
                    throw new Exception("UserId not found");
                }
                var user = new GetViewUser
                {
                    Id = UserId,
                    Name = userEntity.Name,
                    Email = userEntity.Email,
                    Address = userEntity.Address,
                    City = userEntity.City,
                    Phone = userEntity.Phone,
                    DateOfBirth = userEntity.DateOfBirth,
                };
                return user;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<UpdateGetViewUser> UpdateViewUser(int userId, UpdateGetViewUser updateUserDto)
        {
            try
            {
                var user = await _DbContex.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (user == null)
                {
                    throw new Exception("userId not found");
                }

                if (!string.IsNullOrEmpty(updateUserDto.Name) && updateUserDto.Name != "null")
                {
                    user.Name = updateUserDto.Name;
                }
                if (!string.IsNullOrEmpty(updateUserDto.Email) && updateUserDto.Email != "null")
                {
                    user.Email = updateUserDto.Email;
                }
                if (!string.IsNullOrEmpty(updateUserDto.Address) && updateUserDto.Address != "null")
                {
                    user.Address = updateUserDto.Address;
                }
                if (!string.IsNullOrEmpty(updateUserDto.City) && updateUserDto.City != "null")
                {
                    user.City = updateUserDto.City;
                }
                if (!string.IsNullOrEmpty(updateUserDto.Phone) && updateUserDto.Phone != "null")
                {
                    user.Phone = updateUserDto.Phone;
                }
                if (!string.IsNullOrEmpty(updateUserDto.DateOfBirth) && updateUserDto.DateOfBirth != "null")
                {
                    user.DateOfBirth = updateUserDto.DateOfBirth;
                }

                await _DbContex.SaveChangesAsync();
                return updateUserDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating View User: {ex.Message}");
            }
        }
    }
}
