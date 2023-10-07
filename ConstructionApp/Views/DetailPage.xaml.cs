using ConstructionApp.ViewModel;

namespace ConstructionApp.Pages;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}