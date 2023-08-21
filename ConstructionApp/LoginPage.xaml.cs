using ConstructionApp.DataServices.Interface;
using ConstructionApp.Model;
using ConstructionApp.Pages;
using ConstructionApp.Services.Interface;
using System.Diagnostics;
using System.Globalization;

namespace ConstructionApp;

public partial class LoginPage : ContentPage
{                                    
    private readonly ILoginDataService _loginDataService;
    public LoginPage(ILoginDataService loginDataService)
    {
        InitializeComponent();
        _loginDataService = loginDataService;
    }

    async void Login_Clicked(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        if(userName == null || password == null)
        {
            await DisplayAlert("Warning", "Please input username and password", "Ok");
            return;
        }
        LoginModel userinfo = await _loginDataService.LoginUser(userName, password);
        if(userinfo != null)
        {
            await Navigation.PushAsync(new HomePage());
        }
        else
        {
            await DisplayAlert("Warning", "Username or Password is incorrect", "Ok");
        }
    }

}