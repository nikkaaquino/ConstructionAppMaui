using ConstructionApp.Model.Response;

namespace ConstructionApp.Services.Interface
{
    public interface IPhotoDataService
    {
        Task<List<PhotoModel>> GetAllPhotosAsync(string username);
        Task AddPhotoAsync(PhotoModel model);
        Task DeletePhotoAsync(int id);
        Task UpdatePhotoAsync(PhotoModel model);
    }
}
