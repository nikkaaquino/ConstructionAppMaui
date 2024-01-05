using ConstructionApp.Pages;

namespace ConstructionApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
		Routing.RegisterRoute(nameof(PhotoListPage), typeof(PhotoListPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
