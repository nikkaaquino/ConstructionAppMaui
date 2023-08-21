using MicromaxApi.Data.Entity;
using MicromaxApi.Model;

namespace MicromaxApi.Data.Repositories.Interface
{
    public interface ICaptureImageRepository
    {
       public Task <bool> SaveImage(CaptureImageEntity entity);

       Task<List<CaptureImageEntity>> GetImageByUser(string userid);

        Task<List<ImageEntity>> GetImages(string userid);

    }
}
