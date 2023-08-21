using ConstructionApp.Model;
using ConstructionApp.Services.Interface;
using System.Diagnostics;

namespace ConstructionApp.Pages;

public partial class SavePhoto : ContentPage
{

    private readonly IImageDataService _service;
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
    public SavePhoto()
	{
		InitializeComponent();
	}

    public SavePhoto(IImageDataService service)
    {
        _service = service;
        BindingContext = this;
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
        await _service.AddImageAsync(CaptureImage);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        myImage.Source = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.JPEG);
    }
}