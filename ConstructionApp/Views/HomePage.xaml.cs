namespace ConstructionApp.Pages;


public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //myImage.Source = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.JPEG);
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult myPhoto = await MediaPicker.Default.CapturePhotoAsync();
            if(myPhoto != null)
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

    private async void OnPhotoListClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(MainPage)}");
    }
}