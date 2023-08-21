using MicromaxApi.Model;
using MicromaxApi.Services.Config;
using MicromaxApi.Services.Dto;

namespace MicromaxApi.Services.Interface
{
    public interface ICaptureImageService : IErrorService
    {
        Task<bool> SaveImage(CaptureImageModel model);
        Task<List<ImageResponse>> GetImagesByUser(string userid);
    }
}
