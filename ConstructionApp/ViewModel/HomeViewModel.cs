namespace ConstructionApp.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        public PhotoModel photoDetails {  get; set; }

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
                Debug.WriteLine("---> Add New Item");
                await photoDataService.AddPhotoAsync(photoDetails);
                await Shell.Current.DisplayAlert("Information", "Successfully addded", "Ok");
            }catch (Exception ex)
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
