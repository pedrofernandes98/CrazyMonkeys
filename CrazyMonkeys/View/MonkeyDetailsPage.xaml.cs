using CrazyMonkeys.ViewModel;

namespace CrazyMonkeys.View;

public partial class MonkeyDetailsPage : ContentPage
{
	public MonkeyDetailsPage(MonkeyDetailsViewModel monkeyDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = monkeyDetailsViewModel;
	}
}