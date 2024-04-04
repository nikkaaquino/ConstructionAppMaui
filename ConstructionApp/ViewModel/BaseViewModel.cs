﻿using Microsoft.Maui.Devices.Sensors;

namespace ConstructionApp.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => IsBusy;       

        [RelayCommand]
        static async Task Logout()
        {
            await Shell.Current.GoToAsync("//LoginPage");

        }
    }
}
