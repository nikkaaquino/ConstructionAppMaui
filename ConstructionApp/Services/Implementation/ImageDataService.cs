using ConstructionApp.Model;
using ConstructionApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConstructionApp.Services.Implementation
{
    public class ImageDataService : IImageDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializeOptions;

        public ImageDataService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(300);
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5003" : "http://localhost:5003";
            _url = $"{_baseAddress}/api/capture-image/save";

            _jsonSerializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddImageAsync(CaptureImageModel model)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----> No Internet Access ....");
                return;
            }
            try
            {
                string jsontoDo = JsonSerializer.Serialize<CaptureImageModel>(model, _jsonSerializeOptions);
                StringContent content = new StringContent(jsontoDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/api/capture-image/save", content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully saved image");
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

            return;
        }

        public Task DeleteImageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CaptureImageModel>> GetAllImageAsync()
        {
            throw new NotImplementedException();
        }
    }
}
