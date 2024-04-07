using System.Collections.ObjectModel;

namespace ConstructionApp.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public ObservableCollection<PhotoModel> Photos { get; } = new();
        PhotoDataService photoDataService;
        IConnectivity connectivity;

        public MainViewModel(PhotoDataService photoDataService, IConnectivity connectivity)
        {
            this.photoDataService = photoDataService;
            this.connectivity = connectivity;
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string owner = "user";

        [RelayCommand]
        async Task GetPhotosAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No Connectivity",
                        $"Please check internet and try again.", "Ok");
                }
                IsBusy = true;

                var photos = await photoDataService.GetAllPhotosAsync(Owner);

                if (Photos.Count != 0)
                    Photos.Clear();

                foreach (var photo in photos)
                    Photos.Add(photo);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get photos: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally { 
                IsBusy = false; 
                IsRefreshing = false; 
            }

        }

    }
}
