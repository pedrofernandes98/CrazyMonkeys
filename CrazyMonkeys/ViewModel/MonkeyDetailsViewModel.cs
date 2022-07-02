using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrazyMonkeys.Models;
using Microsoft.Maui.ApplicationModel;

namespace CrazyMonkeys.ViewModel
{
    [QueryProperty(nameof(Monkey), "Monkey")]
    public partial class MonkeyDetailsViewModel : BaseViewModel
    {
        IMap map;
        public MonkeyDetailsViewModel(IMap map)
        {
            this.map = map;
        }

        [ObservableProperty]
        Monkey monkey;

        [ICommand]
        async Task ShowLocationOnMapAsync()
        {
            try
            {
                await map.OpenAsync(Monkey.Latitude, Monkey.Longitude,
                    new MapLaunchOptions
                    {
                        Name = Monkey.Name,
                        NavigationMode = NavigationMode.None
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Erro!", 
                    string.Format("Não foi possível exibir o Mapa: {0}", ex.Message), "Ok");

            }
        }
    }
}
