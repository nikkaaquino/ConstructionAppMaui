namespace ConstructionApp.DataServices.Implementation
{
    public class LoginDataService : ILoginDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public LoginDataService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            _url = $"{Constants.BASE_API_URL}/login/AuthenticateUser";
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