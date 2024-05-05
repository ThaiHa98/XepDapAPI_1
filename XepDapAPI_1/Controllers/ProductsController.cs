using Data.Dto;


using Microsoft.AspNetCore.Mvc;
using XepDapAPI_1.Service.Interfaces;
using APIThuVien.Common;
using System.Net;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.AspNetCore.Authorization;
using XepDapAPI_1.Repository.Interface;
using XeDapAPI.Helper;

namespace XepDapAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsIService _productsService;
        private readonly IProductsInterface _productsInterface;
        private readonly Token _token;
        public ProductsController(IProductsIService productsIService,IProductsInterface productsInterface,Token token)
        {
            _productsInterface = productsInterface;
            _productsService = productsIService;
            _token = token;
        }
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProducts([FromForm]ProductDto productsDto,[FromForm]IFormFile image)
        {
            try
            {
                if (productsDto == null)
                {
                    return Unauthorized("Invalid slide data");
                }
                var product = _productsService.Create(productsDto, image);
                return Ok(new XBaseResult
                {
                    data = product,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = product.Id,
                    message = "Create Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)(HttpStatusCode.BadRequest),
                    message = ex.Message
                });
            }
        }
        [HttpPut("Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(200)]
        public IActionResult UpdateProduct([FromForm]UpdateProductDto updateproductDto, [FromForm] IFormFile image)
        {
            try
            {
                if(updateproductDto == null)
                {
                    return Unauthorized("Invalid slide data");
                }
                var delete = _productsService.Update(updateproductDto, image);
                return Ok(new XBaseResult
                {
                    data = updateproductDto,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = updateproductDto.Id,
                    message = "Update Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode= (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }

        [HttpGet("GetBrandName")]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetListBrandName([FromQuery] string keyword)
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
                if (keyword == null)
                {
                    return BadRequest(new XBaseResult
                    {
                        success = false,
                        httpStatusCode = (int)HttpStatusCode.BadRequest,
                        message = "Invalid slide data"
                    });
                }

                var query = _productsInterface.GetAllBrandName(keyword);
                return Ok(new XBaseResult
                {
                    data = query,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = query.Count,
                    message = "List"
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
        [HttpGet("GetTypeName")]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetListTypeName([FromQuery] string keyword)
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
                if (keyword == null)
                {
                    return BadRequest(new XBaseResult
                    {
                        success = false,
                        httpStatusCode = (int)HttpStatusCode.BadRequest,
                        message = "Invalid slide data"
                    });
                }
                var typeName = _productsInterface.GetAllTypeName(keyword);
                return Ok(new XBaseResult
                {
                    data = typeName,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = typeName.Count,
                    message = "List"
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
        [HttpGet("GetPriceHasDecreased")]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetListPriceHasDecreased()
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
                var PriceHasDecreased = _productsInterface.SearchProductsByPriceHasDecreased();
                return Ok(new XBaseResult
                {
                    data = PriceHasDecreased,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = PriceHasDecreased.Count,
                    message = "List"
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
