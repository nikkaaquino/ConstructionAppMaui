using api.motorstar.Services.ApiDto;
using Autofac.Core;
using MicromaxApi.Model;
using MicromaxApi.Services.Dto;
using MicromaxApi.Services.Implementation;
using MicromaxApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MicromaxApi.Controllers
{
    [Route("api/capture-image")]
    [ApiController]

    public class CaptureImageController : ControllerBase
    {
        private readonly ICaptureImageService _imageService;

        public CaptureImageController(ICaptureImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        [Route("get-list")]
        public async Task<IActionResult> GetImageByUserAsync(string userid)
        {
            if (ModelState.IsValid)
            {
                var result = await _imageService.GetImagesByUser(userid);
                if (_imageService.IsValid)
                {
                    var response = new ApiResponse<List<ImageResponse>>
                    {
                        Data = result.ToList(),
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest(_imageService.Validation);
                }
            }
            else
            {
                return BadRequest(new ApiErrorResponse(ModelState));
            }
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save([FromBody] CaptureImageModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _imageService.SaveImage(model);
                if (!_imageService.IsValid)
                {
                    return BadRequest(_imageService.Validation);
                }
                else
                {
                    var response = new ApiResponse<Task<bool>>
                    {
                        Data = result
                    };

                    return Ok(response);
                }
            }
            else
            {
                return BadRequest(new ApiErrorResponse(ModelState));
            }
        }
    }
}
