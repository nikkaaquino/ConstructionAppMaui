using ConstructionApp.Services.Interface;
using ConstructionApp.ViewModel;


namespace ConstructionApp.Pages;

public partial class PhotoListPage : ContentPage
{
	private readonly IPhotoDataService _photoDataService;
	public PhotoListPage(IPhotoDataService photoDataService)
	{
		InitializeComponent();
        _photoDataService = photoDataService;
        BindingContext = new PhotoListViewModel();
	}

    protected async override void OnAppearing() 
    {
        base.OnAppearing();
        string userName = txtUserName.Text;
        collectionView.ItemsSource = await _photoDataService.GetAllPhotosAsync();
    }
}