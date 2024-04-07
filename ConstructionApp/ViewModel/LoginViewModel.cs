
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
            if (IsBusy)
                return;

            try {
                IsBusy = true;

                LoginModel userinfo = await loginService.LoginUser(Username, Password);
                if (userinfo != null)
                {
                    Username = string.Empty;
                    Password = string.Empty;

                    await Shell.Current.GoToAsync(nameof(HomePage));
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
