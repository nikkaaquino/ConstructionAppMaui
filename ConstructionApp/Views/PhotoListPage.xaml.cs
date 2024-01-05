using ConstructionApp.Services.Interface;
using ConstructionApp.ViewModel;


namespace ConstructionApp.Pages;

public partial class PhotoListPage : ContentPage
{
    private readonly PhotoListViewModel _viewModel;
	public PhotoListPage(PhotoListViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}