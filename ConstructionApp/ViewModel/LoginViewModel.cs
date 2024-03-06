
namespace ConstructionApp.ViewModel
{
    public partial class LoginViewModel : ObservableObject
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
                    await Shell.Current.GoToAsync($"{nameof(HomePage)}");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Warning", "Username or Password is incorrect", "Ok");
                }

        }

        private bool isRunning = false;
        public bool IsRunning
        {
            set => SetProperty(ref isRunning, value);
            get => isRunning;
        }
    }
}
