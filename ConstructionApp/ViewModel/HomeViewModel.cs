namespace ConstructionApp.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty] public int _imgId;
        [ObservableProperty] public string _imgName;
        [ObservableProperty] public string _imgData;
        [ObservableProperty] public string _loc;
        [ObservableProperty] public string _owner;
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
        IGeolocation geolocation;
        IMap map;

        public HomeViewModel(IPhotoDataService photoDataService, IGeolocation geolocation, IMap map)
        {
            this.photoDataService = photoDataService;
            this.geolocation = geolocation;
            this.map = map;
        }

        [RelayCommand]
        async Task Tap()
        {
            //TODO: refresh page without details
            await Shell.Current.GoToAsync("//LoginPage");

        }

        public async Task<String> LoadPhotoAsync(FileResult photo)
        {
            var stream = photo.OpenReadAsync().Result;

            byte[] imagedata;

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imagedata = ms.ToArray();
            }

            var location =  await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
            });

            var addPhoto = new PhotoModel
            {
                ImageId = 03062024,
                ImageName = "imgname_nikka0306202",
                ImageData = "img_datanikka03062024",
                Location = location.Longitude + "," + location.Latitude,
                User = "mmaquino",
                ImageType = "png",
                ImageView = imagedata,
            };

            await photoDataService.AddPhotoAsync(addPhoto);

            var folderpath = Path.Combine(FileSystem.AppDataDirectory, "Photo");
            if (!File.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            var empfilename = Guid.NewGuid() + "_photo.jpg";

            var newfile = Path.Combine(folderpath, empfilename);// Complete Path of the photo

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
