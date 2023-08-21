using ConstructionApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionApp.Services.Interface
{
    public interface IImageDataService
    {
        Task<List<CaptureImageModel>> GetAllImageAsync();
        Task AddImageAsync(CaptureImageModel model);
        Task DeleteImageAsync(int id);

    }
}
