namespace ConstructionApp.Services.Implementation
{
    public class PhotoDataService : IPhotoDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializeOptions;

        List<PhotoModel> _photos;

        public PhotoDataService()
        {
            _httpClient = new HttpClient();
            _url = Constants.BASE_API_URL;

            _jsonSerializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public Task DeletePhotoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhotoModel>> GetAllPhotosAsync(string username)
        {
            //if (_photos?.Count > 0)
            //    return _photos;

            var response = await _httpClient.GetAsync($"{_url}/image/image-list?UserId={username}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                _photos = JsonSerializer.Deserialize<List<PhotoModel>>(content, _jsonSerializeOptions);
            }

            return _photos;
            
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

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/image/save-image",content);
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
