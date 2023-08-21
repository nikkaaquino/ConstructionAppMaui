using ConstructionApp.Model;
using ConstructionApp.Services.Interface;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace ConstructionApp.Pages;


public partial class HomePage : ContentPage
{

    private readonly IImageDataService _imageService;
    CaptureImageModel _image;
    bool _isNew;

    public CaptureImageModel CaptureImage
    {
        get => _image;
        set
        {
            _isNew = IsNew(value);
            _image = value;
            OnPropertyChanged();
        }
    }

    public HomePage()
    {
        InitializeComponent();
    }
    public HomePage(IImageDataService imageService)
	{
		InitializeComponent();
        _imageService = imageService;
    }

    bool IsNew(CaptureImageModel model)
    {
        if (model.ImageId == 0)
            return true;
        return false;
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        cameraView.Camera = cameraView.Cameras.First();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StopCameraAsync();
            await cameraView.StartCameraAsync();
        });
    }

    private void Retake_Clicked(object sender, EventArgs e)
    {

    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("---> Add New Item");
        await _imageService.AddImageAsync(CaptureImage);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        myImage.Source = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.JPEG);
    }

}