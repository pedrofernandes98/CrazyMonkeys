using CommunityToolkit.Mvvm.ComponentModel;
using CrazyMonkeys.Models;

namespace CrazyMonkeys.ViewModel
{
    [QueryProperty(nameof(Monkey), "Monkey")]
    public partial class MonkeyDetailsViewModel : BaseViewModel
    {
        public MonkeyDetailsViewModel()
        {

        }

        [ObservableProperty]
        Monkey monkey;
    }
}
