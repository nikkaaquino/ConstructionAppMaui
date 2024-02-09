namespace ConstructionApp.DataServices.Implementation
{
    public class LoginDataService : ILoginDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializeOptions;

        public LoginDataService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(300);
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5003" : "http://localhost:5003";
            _url = $"{_baseAddress}/api/login/AuthenticateUser";

            _jsonSerializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<LoginModel> LoginUser(string username, string password)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----> No Internet Access ....");
                return null;
            }
            try
            {       
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}?UserId={username}&Password={password}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var userInfo = JsonSerializer.Deserialize<LoginModel>(content);
                    return userInfo;
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                    return null;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Oops exception: {ex.Message}");
            }

            return null;
        }
    }
}