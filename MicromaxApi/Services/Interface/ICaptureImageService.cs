using MicromaxApi.Model;
using MicromaxApi.Services.Config;
using MicromaxApi.Services.Dto;

namespace MicromaxApi.Services.Interface
{
    public interface ICaptureImageService : IErrorService
    {
        Task<bool> SaveImages(ImageModel model);
        Task<List<ImagesResponse>> GetImages(string userid);
    }
}
