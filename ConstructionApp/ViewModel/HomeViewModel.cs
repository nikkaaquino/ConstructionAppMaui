namespace ConstructionApp.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty] public string _imgName;
        [ObservableProperty] public byte[] _imgData;
        [ObservableProperty] public string _loc;
        [ObservableProperty] public byte[] _imgView;
        [ObservableProperty] public string _imgType;

        [ObservableProperty] public CameraView _cameraView;
        [ObservableProperty] ImageSource _imgSrc;


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
        IGeolocation geolocation;
        IMap map;

        public HomeViewModel(IPhotoDataService photoDataService, ILoginDataService loginDataService, IGeolocation geolocation, IMap map)
        {
            this.photoDataService = photoDataService;
            this.loginDataService = loginDataService;
            this.geolocation = geolocation;
            this.map = map;
        }

        [RelayCommand]
        async static Task Logout()
        {
            //remove currrent session for image data on homepage
            await Shell.Current.GoToAsync("//LoginPage");
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


        [RelayCommand]
        async Task CapturePhoto()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                CompletePhotoPath = await LoadPhotoAsync(photo);

                Console.WriteLine("Photo Captured" + CompletePhotoPath);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        [RelayCommand]
        async Task GoToPhotoDetails()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }
    }
}