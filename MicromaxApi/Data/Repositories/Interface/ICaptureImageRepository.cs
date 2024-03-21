using MicromaxApi.Data.Entity;
using MicromaxApi.Model;

namespace MicromaxApi.Data.Repositories.Interface
{
    public interface ICaptureImageRepository
    {
        public Task<bool> SaveImages(ImageEntity entity);

        Task<List<ImageEntity>> GetImages(string userid);

    }
}
