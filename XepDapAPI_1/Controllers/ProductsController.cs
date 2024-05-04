using Data.Dto;
using Data.Models.Enum;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using XeDapAPI.Helper;
using XepDapAPI_1.Repository.Interface;
using XepDapAPI_1.Service.Interfaces;

namespace XepDapAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsIService _productsService;
        public ProductsController(IProductsIService productsIService)
        {
            _productsService = productsIService;
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
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
