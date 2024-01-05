using ConstructionApp.Services.Implementation;

namespace ConstructionApp.ViewModel
{
    public partial class PhotoListViewModel : BaseViewModel
    {
        public ObservableCollection<PhotoModel> Photos { get; } = new();
        PhotoDataService photoDataService;
        IConnectivity connectivity;

        public PhotoListViewModel(PhotoDataService photoDataService, IConnectivity connectivity)
        {
            Title = "Construction Appliction";
            this.photoDataService = photoDataService;
            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task GoToDetails(PhotoModel photos)
        {
            if (photos == null)
                return;

        await Shell.Current.GoToAsync(nameof(DetailPage), true, new Dictionary<string, object>
        {
            {"Photos", photos }
        });
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetPhotosAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if(connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!", 
                        $"Please check internet and try again", "OK");
                    return;
                }
                IsBusy = true;
                var photos = await photoDataService.GetPhotos();

                if (Photos.Count != 0)
                    Photos.Clear();

                foreach(var photo in photos)
                    Photos.Add(photo);

            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
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
