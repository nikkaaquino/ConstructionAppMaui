namespace ConstructionApp.ViewModel
{
    [QueryProperty("Owner", "Owner")]
    public partial class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<PhotoModel> Photos { get; } = new();

        IPhotoDataService photoDataService;
        IConnectivity connectivity;
        IGeolocation geolocation;
        IMap map;

        public HomeViewModel(IPhotoDataService photoDataService, IGeolocation geolocation, IMap map, IConnectivity connectivity)
        {
            this.photoDataService = photoDataService;
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

                if (Photos.Count == 0)
                    await Shell.Current.DisplayAlert("Information", "No Photos added yet, please click Capture Photo to proceed.", "Ok");

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
        async Task Appearing()
        {
            try
            {
                await GetPhotosAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task CapturePhoto()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var photo = await MediaPicker.CapturePhotoAsync();
                await SavePhotoAsync(photo);
                await Shell.Current.DisplayAlert("Information", "Successfully added!", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to capture photo: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");

            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public async Task SavePhotoAsync(FileResult photo)
        {
            try
            {
                var stream = photo.OpenReadAsync().Result;

                byte[] imagedata;

                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    imagedata = ms.ToArray();
                }

                var empfilename = Guid.NewGuid() + "_photo.jpg";

                var location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                var curlocation = await GetGeocodeReverseData(location.Latitude, location.Longitude);

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

                var addPhoto = new PhotoModel
                {
                    ImageName = empfilename,
                    ImageData = imagedata,
                    Location = curlocation,
                    User = Owner,
                    ImageType = photo.ContentType,
                    ImagePath = newfile,
                };

                await photoDataService.AddPhotoAsync(addPhoto);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to capture photo: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }

            async Task<string> GetGeocodeReverseData(double latitude, double longitude)
            {
                IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

                Placemark placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    return ($"{placemark.Locality},{placemark.SubAdminArea},{placemark.AdminArea}, {placemark.CountryName}");

                    /*
                AdminArea: Calabarzon
                CountryCode:     PH
                CountryName:     Philippines
                FeatureName:     CXP3 + 65R
                Locality:        Bacoor
                PostalCode:      
                SubAdminArea: Cavite
                SubLocality:     
                SubThoroughfare:
            Thoroughfare:
                return
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";*/
                }

                return "";
            }

        }
    }
}