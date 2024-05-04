using APIThuVien.Common;
using Data.Dto;
using Data.Models;
using Data.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using XeDapAPI.Helper;
using XepDapAPI_1.Service.Interfaces;

namespace XepDapAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandIService _brandIService;
        private readonly Token _token;
        public BrandController(IBrandIService brandService, Token token)
        {
            _token = token;
            _brandIService = brandService;
        }
        [HttpPost("Create")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreteBrand(Brand brand)
        {
            try
            {
                if (brand == null)
                {
                    return Unauthorized("Invalid slide data");
                }
                var create = _brandIService.Create(brand);
                return Ok(new XBaseResult
                {
                    data = create,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = brand.Id,
                    message = "Brand Successfully"
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
        [HttpDelete("delete")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult DeleteBrand(int Id)
        {
            try
            {
                if (Id == null)
                {
                    return Unauthorized("id not found");
                }
                var delete = _brandIService.Delete(Id);
                return Ok(delete);
            }
            catch(Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateBrand(BrandDto brandDto)
        {
            try
            {
                if (brandDto == null)
                {
                    return Unauthorized("id not found");
                }
                var update = _brandIService.Update(brandDto);
                return Ok(new XBaseResult
                {
                    data = update,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = brandDto.Id,
                    message = "Update Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }

    }
}
