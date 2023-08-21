using MicromaxApi.Data.Entity;
using MicromaxApi.Data.Repositories.Interface;
using MicromaxApi.Services.Config;
using MicromaxApi.Services.Dto;
using MicromaxApi.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MicromaxApi.Services.Implementation
{
    public class LoginService : ErrorService, ILoginService
    {
        public ILoginRepository _loginRepo;

        public LoginService(ILoginRepository loginRepo)
        {
            _loginRepo = loginRepo;
        }
        public async Task<LoginResponse> GetLoginById(string id)
        {
            try
            {
                var result = await _loginRepo.GetById(id);

                var response = new LoginResponse
                {
                    UserId = result.UserId,
                    UsrPassword = result.UsrPassword
                };
                return response;

            }
            catch (Exception ex)
            {
                Validation.Add("errors", "Something went wrong");
                return null;
            }
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            try
            {
                var result = await _loginRepo.GetById(username);
                if (result.UserId == username && result.UsrPassword == password)
                {
                    var response = new LoginResponse
                    {
                        UserId = result.UserId,
                        UsrPassword = result.UsrPassword
                    };
                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                Validation.Add("errors", "Invalid Username or Password");
                return null;
            }

        }
    }
}
