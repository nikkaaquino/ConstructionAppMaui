using MicromaxApi.Data.Entity;
using MicromaxApi.Services.Config;
using MicromaxApi.Services.Dto;

namespace MicromaxApi.Services.Interface
{
    public interface ILoginService : IErrorService
    {
        Task<LoginResponse> GetLoginById(string id);
        Task<LoginResponse> LoginAsync(string username, string password);
        //Task<bool> LoginUserAsync(string username, string password);
    }
}
