using api.motorstar.Services.ApiDto;
using MicromaxApi.Data.Repositories.Interface;
using MicromaxApi.Model;
using MicromaxApi.Services.Dto;
using MicromaxApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MicromaxApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("AuthenticateUser")]
        public async Task<IActionResult> AuthorizeUserAsync([FromQuery] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginService.LoginAsync(model);
                if (_loginService.IsValid)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_loginService.Validation);
                }
            }
            else
            {
                return BadRequest(new ApiErrorResponse(ModelState));
            }
        }

        [HttpGet]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUserAsync(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginService.LoginAsync(username, password);
                if (_loginService.IsValid)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_loginService.Validation);
                }
            }
            else
            {
                return BadRequest(new ApiErrorResponse(ModelState));
            }

        }


    }
}
