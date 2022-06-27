using CrazyMonkeys.ViewModel;

namespace CrazyMonkeys;

public partial class MainPage : ContentPage
{
	public MainPage(MonkeyViewModel monkeyViewModel)
	{
		InitializeComponent();
		this.BindingContext = monkeyViewModel;
	}
}

