using ConstructionApp.Model;
using ConstructionApp.Services.Interface;
using System.Diagnostics;
using System.Text.Json;

namespace ConstructionApp.Services.Implementation
{
    public class PhotoDataService : IPhotoDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializeOptions;

        public PhotoDataService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(300);
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5003" : "http://localhost:5003";
            _url = $"{_baseAddress}/api/image";

            _jsonSerializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public Task AddPhotoAsync(PhotoModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeletePhotoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhotoModel>> GetAllPhotosAsync(string username)
        {
            List<PhotoModel> photo = new List<PhotoModel>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----> No Internet Access ....");
                return photo;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/image-list?username={username}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    photo = JsonSerializer.Deserialize<List<PhotoModel>>(content, _jsonSerializeOptions);
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Oops exception: {ex.Message}");
            }

            return photo;
        }
    }
}
