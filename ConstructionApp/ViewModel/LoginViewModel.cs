
namespace ConstructionApp.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        ILoginDataService loginService;
        IConnectivity connectivity;

        public LoginViewModel(ILoginDataService loginService, IConnectivity connectivity)
        {
            this.loginService = loginService;
            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task Login()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No Connectivity",
                    $"Please check internet and try again.", "Ok");
            }
            if (IsBusy)
                return;

            try {
                IsBusy = true;

                LoginModel userinfo = await loginService.LoginUser(Username, Password);
                if (userinfo != null)
                {
                    await Shell.Current.GoToAsync($"{nameof(HomePage)}?Owner={Username}");
                    Username = string.Empty;
                    Password = string.Empty;
                }
            }
            catch(Exception ex) {
                Debug.WriteLine($"Unable to Login: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }       
           

        }
    }
}
