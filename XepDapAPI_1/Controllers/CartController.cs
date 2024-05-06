using APIThuVien.Common;
using Data.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using XepDapAPI_1.Service.Interfaces;

namespace XepDapAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartIService _cartService;
        public CartController(ICartIService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("CreatCart")]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateCart([FromBody] List<CartDto> cartDtoList)
        {
            try
            {
                //var userClaims = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                //if (userClaims != null && int.TryParse(userClaims.Value, out int userID))
                //{
                //    var tokenStatus = _token.CheckTokenStatus(userID);
                //    if (tokenStatus == StatusToken.Expired)
                //    {
                //        return Unauthorized("The token is no longer valid. Please log in again.");
                //    }
                //}
                foreach (var cartDto in cartDtoList)
                {
                    var create = _cartService.CrateBicycle(cartDto);
                   
                }
                return Ok(new XBaseResult
                {
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = cartDtoList.Count,
                    message = "Create Successfully"
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
        [HttpPut("IncreaseQuantityShoppingCart")]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult IncreaseQuantityShoppingCart(int UserId,int createProductId)
        {
            try
            {
                //var userClaims = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                //if (userClaims != null && int.TryParse(userClaims.Value, out int userID))
                //{
                //    var tokenStatus = _token.CheckTokenStatus(userID);
                //    if (tokenStatus == StatusToken.Expired)
                //    {
                //        return Unauthorized("The token is no longer valid. Please log in again.");
                //    }
                //}
                if(UserId == null && createProductId == null)
                {
                    throw new Exception("UserId && createProductId not found");
                }
                var increase = _cartService.IncreaseQuantityShoppingCart(UserId, createProductId);
                return Ok(new XBaseResult
                {
                    data = increase,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpPut("ReduceShoppingCart")]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult ReduceShoppingCart(int UserId,int createProductId)
        {
            try
            {
                //var userClaims = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                //if (userClaims != null && int.TryParse(userClaims.Value, out int userID))
                //{
                //    var tokenStatus = _token.CheckTokenStatus(userID);
                //    if (tokenStatus == StatusToken.Expired)
                //    {
                //        return Unauthorized("The token is no longer valid. Please log in again.");
                //    }
                //}
                if (UserId == null && createProductId == null)
                {
                    throw new Exception("UserId && createProductId not found");
                }
                var query = _cartService.ReduceShoppingCart(UserId, createProductId);
                return Ok(new XBaseResult
                {
                    data = query,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "ReduceShoppingCart Successfully"
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
    }
}
