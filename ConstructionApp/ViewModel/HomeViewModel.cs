using ConstructionApp.Services.Interface;

namespace ConstructionApp.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty] public int _imgId;
        [ObservableProperty] public string _imgName;
        [ObservableProperty] public string _imgData;
        [ObservableProperty] public string _loc;
        [ObservableProperty] public string _owner;
        //[ObservableProperty] public string _imgView;
        [ObservableProperty] public string _imgType;


        IPhotoDataService photoDataService;
        public HomeViewModel(IPhotoDataService photoDataService)
        {
            this.photoDataService = photoDataService;
        }

        [RelayCommand]
        async Task Tap()
        {
            await Shell.Current.GoToAsync("//LoginPage");

        }

        [RelayCommand]
        async Task SavePhoto()
        {
            try
            {
                var addPhoto = new PhotoModel
                {
                    ImageId = ImgId,
                    ImageName = ImgName,
                    ImageData = ImgData,
                    Location = Loc,
                    User = Owner,
                    ImageType = ImgType,
                    //ImageView = ImgView,
                };

                Debug.WriteLine("---> Add New Item");
                await photoDataService.AddPhotoAsync(addPhoto);
                await Shell.Current.DisplayAlert("Information", "Successfully addded", "Ok");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        async Task Capture()
        {
            //myImage.Source = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.JPEG);
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
                if (myPhoto != null)
                {
                    //save the image capture in the application
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                    using Stream sourceStream = await myPhoto.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("OOPS", "Your device isn't supported", "Ok");
            }
        }

        [RelayCommand]
        async Task GoToPhotoDetails()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }
    }
}
