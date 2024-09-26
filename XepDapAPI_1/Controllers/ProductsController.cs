﻿using Data.Dto;
using Microsoft.AspNetCore.Mvc;
using XepDapAPI_1.Service.Interfaces;
using APIThuVien.Common;
using System.Net;
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
        public async Task<IActionResult> CreateProducts([FromForm] ProductDto productsDto)
        {
            try
            {
                if (productsDto == null)
                {
                    return BadRequest(new XBaseResult
                    {
                        success = false,
                        httpStatusCode = (int)HttpStatusCode.BadRequest,
                        message = "Invalid product data"
                    });
                }
                // Tạo sản phẩm
                var product = await _productsService.Create(productsDto);

                return Ok(new XBaseResult
                {
                    data = product,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = product.Id,
                    message = "Product created successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = "An error occurred while creating the product: " + ex.Message
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
        public IActionResult GetListTypeName([FromQuery] string keyword,int limit = 8)
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
                var typeName = _productsInterface.GetAllTypeName(keyword,limit);
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
        [HttpGet("images/product/{productId}")]
        public IActionResult GetProductImage(int productId)
        {
            try
            {
                var product = _productsInterface.GetProductsId(productId);

                if (product == null || string.IsNullOrEmpty(product.Image))
                {
                    return NotFound("Image not found!");
                }

                var imageBytes = _productsService.GetProductImageBytes(product.Image);
                return File(imageBytes, "image/jpeg"); // Điều chỉnh loại nội dung tùy thuộc
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
            }
        }

        [HttpGet("GetAllProduct")]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllProduct()
        {
            try
            {
                var products = _productsInterface.GetAllProducts();
                return Ok(new XBaseResult
                {
                    data = products,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = products.Count,
                    message = "List",
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpGet("GetListPrice")]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetListPrice([FromQuery]int minPrice,[FromQuery]int maxPrice)
        {
            try
            {   if(minPrice == null || maxPrice == null)
                {
                    throw new ArgumentNullException("minPrice & maxPrice not found");
                }
                var getPrice = _productsService.GetProductsInPriceRange(minPrice, maxPrice);
                return Ok(new XBaseResult
                {
                    data = getPrice,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = getPrice.Count(),
                    message = "List",
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
        [HttpGet("product/{productID}")]
        public IActionResult getproductID(int productID)
        {
            var get = _productsInterface.GetProductsId(productID);
            return Ok(get);
        }
    }
}
