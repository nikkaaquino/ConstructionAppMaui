using ConstructionApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionApp.Services.Interface
{
    public interface IPhotoDataService
    {
        Task<List<PhotoModel>> GetAllPhotosAsync(string username);
        Task AddPhotoAsync(PhotoModel model);
        Task DeletePhotoAsync(int id);
    }
}
