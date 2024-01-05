using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConstructionApp.Pages;
using Microsoft.Toolkit.Mvvm.Input;

namespace ConstructionApp.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        [RelayCommand]
        async Task Tap()
        {
            //await Shell.Current.GoToAsync($"{nameof(PhotoListPage)}");
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }
    }
}
