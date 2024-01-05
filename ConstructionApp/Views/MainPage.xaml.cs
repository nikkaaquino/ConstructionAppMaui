namespace ConstructionApp.ViewModel;

public partial class MainPage : ContentPage
{
	public MainPage(MonkeysViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}