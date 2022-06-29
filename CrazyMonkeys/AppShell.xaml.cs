using CrazyMonkeys.View;

namespace CrazyMonkeys;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MonkeyDetailsPage), typeof(MonkeyDetailsPage));
	}
}
