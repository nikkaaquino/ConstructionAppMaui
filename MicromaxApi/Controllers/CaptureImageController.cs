using api.motorstar.Services.ApiDto;
using MicromaxApi.Model;
using MicromaxApi.Services.Dto;
using MicromaxApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MicromaxApi.Controllers
{
    [Route("api/image")]
    [ApiController]

    public class CaptureImageController : ControllerBase
    {
        private readonly ICaptureImageService _imageService;

        public CaptureImageController(ICaptureImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        [Route("image-list")]
        public async Task<IActionResult> GetImageByUserAsync(string userid)
        {
            if (ModelState.IsValid)
            {
                var result = await _imageService.GetImages(userid);
                if (_imageService.IsValid)
                {
                    var response = new ApiResponse<List<ImagesResponse>>
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
        [Route("image")]
        public IActionResult Save([FromBody] ImageModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _imageService.SaveImages(model);
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

        [HttpGet]
        [Route("get-list")]
        public async Task<IActionResult> GetImages(string userid)
        {
            if (ModelState.IsValid)
            {
                var result = await _imageService.GetImages(userid);
                if (_imageService.IsValid)
                {
                    var response = new ApiResponse<List<ImagesResponse>>
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
    }
}
