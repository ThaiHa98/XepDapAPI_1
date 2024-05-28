using APIThuVien.Common;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using XeDapAPI.Service.Interfaces;
using XeDapAPI.Dto;
using XeDapAPI.Helper;
using XeDapAPI.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Data.Models.Enum;
using Microsoft.AspNetCore.Authorization;

namespace XeDapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserIService _userIService;
        private readonly Token _token;
        public UserController(IUserIService userIService, Token token)
        {
            _userIService = userIService;
            _token = token;
        }
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateLogin(User user)
        {
            try
            {
                var create = _userIService.RegisterUser(user);
                return Ok(new XBaseResult
                {
                    data = create,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = user.Id,
                    message = "User successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult LoginUser([FromBody] RequestDto requestDto)
        {
            try
            {
                if (requestDto == null || string.IsNullOrEmpty(requestDto.Email) || string.IsNullOrEmpty(requestDto.Password))
                {
                    return BadRequest("Email and password are required.");
                }
                User user = _userIService.Login(requestDto);
                string jwtToken = _token.CreateToken(user);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(3000),
                };
                HttpContext.Response.Cookies.Append("authenticationToken", jwtToken, cookieOptions);
                return Ok(jwtToken);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Logout()
        {
            try
            {
                // Lấy thông tin người dùng từ token
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    var tokenStatus = _token.CheckTokenStatus(userId);

                    if (tokenStatus == StatusToken.Expired)
                    {
                        // Token không còn hợp lệ, từ chối yêu cầu
                        return Unauthorized("The token is no longer valid. Please log in again.");
                    }
                    // Gọi service để đăng xuất người dùng
                    var result = _userIService.logout(userId);

                    if (result)
                    {
                        // Xóa cookei người dùng
                        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return Ok("Logged out successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred during logout.");
                    }
                }
                else
                {
                    return BadRequest("Invalid user ID.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
