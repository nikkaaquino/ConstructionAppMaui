
using ConstructionApp.Services.Interface;
using System.Net.Http.Json;

namespace ConstructionApp.Services.Implementation
{
    public class PhotoDataService
    {
        HttpClient httpClient;

        public PhotoDataService()
        {
            this.httpClient = new HttpClient();
        }

        List<PhotoModel> photos;

        public async Task<List<PhotoModel>> GetPhotos()
        {
            if (photos?.Count > 0)
                return photos;
            var username = "mmaquino";
            var response = await httpClient.GetAsync($"http://localhost:5003/api/image/image-list?username={username}");
            if (response.IsSuccessStatusCode)
            {
                photos = await response.Content.ReadFromJsonAsync(PhotoModelContext.Default.ListPhotoModel);
            }

            return photos;
        }
    }
}
