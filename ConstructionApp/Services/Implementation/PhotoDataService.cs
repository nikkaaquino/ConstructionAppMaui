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
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5003" : "http://localhost:5003";
            _url = $"{_baseAddress}/api";

            _jsonSerializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public Task DeletePhotoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PhotoModel>> GetAllPhotosAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task AddPhotoAsync(PhotoModel model)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----> No Internet Access ....");
                return;
            }
            try
            {
                string jsonPhoto = JsonSerializer.Serialize<PhotoModel>(model, _jsonSerializeOptions);
                StringContent content = new StringContent(jsonPhoto, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/image/image",content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created photo");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }catch(Exception ex)
            {
                Debug.WriteLine($"Oops exception: {ex.Message}");
            }

            return;
        }

        public Task UpdatePhotoAsync(PhotoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
