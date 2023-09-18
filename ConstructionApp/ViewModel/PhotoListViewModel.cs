using CommunityToolkit.Mvvm.ComponentModel;
using ConstructionApp.Pages;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ConstructionApp.ViewModel
{
    public partial class PhotoListViewModel : ObservableObject
    {
        public PhotoListViewModel() 
        {
            Items = new ObservableCollection<string>();
        
        }

        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        [ICommand]
        void Add()
        {
            if (string.IsNullOrEmpty(Text))
                return;

            Items.Add(Text);
            Text = string.Empty;
        }

        [ICommand]
        void Delete(string s)
        {
            if (Items.Contains(s))
            {
                Items.Remove(s);
            }
        }

        //[ICommand]
        //async Task Tap(string s)
        //{ 
        //    await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?Text={s}");
        //}
    }
}
