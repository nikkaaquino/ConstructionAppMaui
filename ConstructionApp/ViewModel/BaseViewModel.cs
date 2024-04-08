using Microsoft.Maui.Devices.Sensors;

namespace ConstructionApp.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string owner;

        [ObservableProperty]
        bool isRefreshing;

        public bool IsNotBusy => IsBusy;


        [RelayCommand]
        static async Task Logout()
        {
            bool answer = await Shell.Current.DisplayAlert("Information", "Are you sure you want to logout?", "Yes", "No");
            if (answer == true)
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }

        }

    }
}
