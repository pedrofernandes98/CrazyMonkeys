using CrazyMonkeys.Models;
using CrazyMonkeys.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using CrazyMonkeys.View;

namespace CrazyMonkeys.ViewModel
{
    public partial class MonkeyViewModel : BaseViewModel
    {
        public ObservableCollection<Monkey> MonkeyList { get; } = new ObservableCollection<Monkey>();

        public MonkeyService MonkeyService;

        //public Command GetMonkeysCommand; 

        public MonkeyViewModel(MonkeyService monkeyService)
        {
            Title = "CrazyMonkeys List";
            MonkeyService = monkeyService;
            //GetMonkeysCommand = new Command(async () => await GetMonkeyAsync());
            //GetMonkeysCommand.CanExecute();
        }

        [ICommand]
        private async Task GetMonkeysAsync()
        {
            if (IsBusy)
                return;

            try
            {
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
        
    }
}
