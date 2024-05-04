using APIThuVien.Common;
using Data.Dto;
using Data.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using XeDapAPI.Helper;
using XeDapAPI.Repository.Interface;
using XeDapAPI.Service.Interfaces;

namespace XeDapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlideController : ControllerBase
    {
        private readonly ISlideIService _slideIService;
        private readonly Token _token;
        private readonly ISlideInterface _slideInterface;
        public SlideController(ISlideIService slideIService, Token token, ISlideInterface slideInterface)
        {
            _slideInterface = slideInterface;
            _token = token;
            _slideIService = slideIService;
        }
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateSlide([FromForm] IFormFile image,[FromForm] SlideDto slideDto)
        {
            try
            {
                if (slideDto == null)
                {
                    return BadRequest("Invalid slide data");
                }
                var create = _slideIService.Create(slideDto, image);
                return Ok(new XBaseResult
                {
                    data = create,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = create.Count(),
                    message = "Create Slide successfully"
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
        /// <summary>
        /// Update slide Url Sort
        /// </summary>
        /// <param name="updateSlideDto"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateSlide([FromBody]UpdateSlideDto updateSlideDto)
        {
            try
            {
                if (updateSlideDto == null)
                {
                    return BadRequest("Invalid slide data");
                }
                var update = _slideIService.Update(updateSlideDto);
                return Ok(new XBaseResult
                {
                    data = update,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = update.Count(),
                    message = "Update Slide successfully"
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
        [HttpDelete("Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult deleteSlide(int Id)
        {
            if(Id == null)
            {
                throw new ArgumentNullException(nameof(Id),"Slide not found");
            }
            var delete = _slideIService.Delete(Id);
            return Ok(delete);
        }
        [HttpGet("GetList")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult getSlide() 
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
                var getlist = _slideInterface.GetSlides();
                return Ok(getlist);
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
