using ConstructionApp.Pages;

namespace ConstructionApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));

    }
}
