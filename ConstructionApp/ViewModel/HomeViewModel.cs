using ConstructionApp.Views;
using Microsoft.Toolkit.Mvvm.Input;

namespace ConstructionApp.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        [RelayCommand]
        async Task Tap()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}
