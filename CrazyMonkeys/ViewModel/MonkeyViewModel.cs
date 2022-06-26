using CrazyMonkeys.Models;
using CrazyMonkeys.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace CrazyMonkeys.ViewModel
{
    public partial class MonkeyViewModel : BaseViewModel
    {
        public ObservableCollection<Monkey> MonkeyList { get; } = new ObservableCollection<Monkey>();

        public MonkeyService MonkeyService;

        //public ICommand GetMonkeysCommand; 

        public MonkeyViewModel(MonkeyService monkeyService)
        {
            Title = "CrazyMonkeys List";
            MonkeyService = monkeyService;
            //GetMonkeysCommand = new Command(async () => await GetMonkeyAsync());
        }

        [ICommand]
        public async Task GetMonkeyAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var monkeys = await MonkeyService.GetMonkeys();
                
                if (MonkeyList.Count > 0)
                    MonkeyList.Clear();

                foreach (var monkey in monkeys)
                    MonkeyList.Add(monkey);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Não foi possível completar a requisição: {ex.Message}");
                await Shell.Current.DisplayAlert("Erro!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
