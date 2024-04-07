namespace ConstructionApp.DataServices.Implementation
{
    public class LoginDataService : ILoginDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializeOptions;
        LoginModel _userinfo;
        ErrorMessage _error;

        public LoginDataService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            _url = $"{Constants.BASE_API_URL}/login/AuthenticateUser";
            _jsonSerializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<LoginModel> LoginUser(string username, string password)
        {
            var response = await _httpClient.GetAsync($"{_url}?UserId={username}&Password={password}");
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {                
                _userinfo = JsonSerializer.Deserialize<LoginModel>(content, _jsonSerializeOptions);
            }
            else
            {
                _error = JsonSerializer.Deserialize<ErrorMessage>(content, _jsonSerializeOptions);
                throw new Exception(_error.Errors);
            }

            return _userinfo;
        }
    }
}