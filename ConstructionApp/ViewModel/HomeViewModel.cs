namespace ConstructionApp.ViewModel
{
    public partial class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<PhotoModel> Photos { get; } = new();
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string owner = "user";

        private string photoPath;

        public string CompletePhotoPath
        {
            get => photoPath;
            set
            {
                SetProperty(ref photoPath, value);
                HasPhoto = !string.IsNullOrEmpty(value);
            }
        }

        private bool _hasPhoto;
        public bool HasPhoto
        {
            get => _hasPhoto;
            set => SetProperty(ref _hasPhoto, value);

        }

        IPhotoDataService photoDataService;
        ILoginDataService loginDataService;
        IConnectivity connectivity;
        IGeolocation geolocation;
        IMap map;

        public HomeViewModel(IPhotoDataService photoDataService, ILoginDataService loginDataService, IGeolocation geolocation, IMap map, IConnectivity connectivity)
        {
            this.photoDataService = photoDataService;
            this.loginDataService = loginDataService;
            this.geolocation = geolocation;
            this.map = map;
            this.connectivity = connectivity;
        }

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
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }

        }

        [RelayCommand]
        async Task CapturePhoto()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                CompletePhotoPath = await LoadPhotoAsync(photo);
                await Shell.Current.DisplayAlert("Information", "Successfully added!", "OK");

                Console.WriteLine("Photo Captured" + CompletePhotoPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to capture photo: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        public async Task<String> LoadPhotoAsync(FileResult photo)
        {
                var stream = photo.OpenReadAsync().Result;

                byte[] imagedata;

                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    imagedata = ms.ToArray();
                }

                var location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                var empfilename = Guid.NewGuid() + "_photo.jpg";

                var addPhoto = new PhotoModel
                {
                    ImageName = empfilename,
                    ImageData = imagedata,
                    Location = location.Latitude + "," + location.Longitude,
                    User = "user", //update this to current user
                    ImageType = photo.ContentType,
                };

                await photoDataService.AddPhotoAsync(addPhoto);

                var folderpath = Path.Combine(FileSystem.AppDataDirectory, "Photo");
                if (!File.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }

                var newfile = Path.Combine(folderpath, empfilename);

                using (var stream2 = new MemoryStream(imagedata))
                using (var newstream = File.OpenWrite(newfile))
                {
                    await stream2.CopyToAsync(newstream);
                }

                return newfile;

        }


        
    }
}