using CrazyMonkeys.Models;
using CrazyMonkeys.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using CrazyMonkeys.View;
using Microsoft.Maui.Devices.Sensors;

namespace CrazyMonkeys.ViewModel
{
    public partial class MonkeyViewModel : BaseViewModel
    {
        public ObservableCollection<Monkey> MonkeyList { get; } = new ObservableCollection<Monkey>();

        public MonkeyService MonkeyService;

        IConnectivity connectivity;

        IGeolocation geolocation;

        //public Command GetMonkeysCommand; 

        public MonkeyViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "CrazyMonkeys List";
            MonkeyService = monkeyService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
            //GetMonkeysCommand = new Command(async () => await GetMonkeyAsync());
            //GetMonkeysCommand.CanExecute();
        }

        private bool HasInternet()
        {
            return connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        [ICommand]
        private async Task GetMonkeysAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if(!HasInternet())
                {
                    throw new Exception("Por favor, verifique a sua internet e tente novamente.");
                }

                IsBusy = true;

                var monkeys = await MonkeyService.GetMonkeys();
                
                if (MonkeyList.Count != 0)
                    MonkeyList.Clear();

                //A lista é pequena, por isso adicionamos um a um e a ObservableCollection chama o método RaisePropertyChanged()
                //Talvez em aplicações que tem mais dados seja mais interessante adicionar toda a lista de uma única vez ou um Range() da lista ...
                foreach (var monkey in monkeys)
                    MonkeyList.Add(monkey);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Não foi possível completar a requisição: {ex.Message}");
                //App.Current.MainPage.DisplayAlert();
                await Shell.Current.DisplayAlert("Erro!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [ICommand]
        private async Task OnClick_Monkey(Monkey monkey)
        {
            if (monkey == null)
                return;
            
            //TODO/TOSEARCH criar uma interface e injetar essa navegação
            await Shell.Current.GoToAsync(nameof(MonkeyDetailsPage), true,
                    new Dictionary<string, object>
                    {
                        {"Monkey", monkey }
                    });
        }

        [ICommand]

        async Task GetClosestMonkeyAsync()
        {
            if (IsBusy || MonkeyList.Count == 0)
                return;

            try
            {
                var userLocation = geolocation.GetLastKnownLocationAsync();

                if (userLocation == null)
                {
                    userLocation = geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });


                }

                var closestMonkey = MonkeyList.OrderBy(m => Location.CalculateDistance(
                    locationStart: userLocation.Result,
                    locationEnd: new Location(m.Latitude, m.Longitude),
                    DistanceUnits.Kilometers))
                    .FirstOrDefault();

                //TODO - Incluir textos puros em um arquivo de Resources
                if (closestMonkey != null)
                    await Shell.Current.DisplayAlert("Macaquinho encontrado",
                        string.Format("O macaquinho mais próximo é o {0} e se encontra no {1}",
                        closestMonkey.Name, closestMonkey.Location), "Ok");
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Não foi possível completar a requisição: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro!", ex.Message, "Ok");
            }
        } 
    }
}
