
namespace ConstructionApp.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        ILoginDataService loginService;

        public LoginViewModel(ILoginDataService loginService)
        {
            this.loginService = loginService;
        }

        [RelayCommand]
        async Task Login()
        {
            if (Username == null || Password == null)
            {
                await Shell.Current.DisplayAlert("Warning", "Please input username and password", "Ok");
                return;
            }

            LoginModel userinfo = await loginService.LoginUser(Username, Password);
            if (userinfo != null)
            {
                Username = string.Empty;
                Password = string.Empty;
                await Shell.Current.GoToAsync($"{nameof(HomePage)}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Warning", "Invalid Username and Password", "Ok");
            }

        }
    }
}
