namespace ConstructionApp.Services.Implementation
{
    public class PhotoDataService : IPhotoDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializeOptions;
        ErrorMessage _error;

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
                    throw new Exception(_error.Errors);
                }
            }
            catch(Exception ex)
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
