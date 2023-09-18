using ConstructionApp.ViewModel;

namespace ConstructionApp.Pages;

public partial class PhotoListPage : ContentPage
{
	public PhotoListPage()
	{
		InitializeComponent();
		BindingContext = new PhotoListViewModel();
	}
}