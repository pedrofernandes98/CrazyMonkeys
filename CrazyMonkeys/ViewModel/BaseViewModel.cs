using CommunityToolkit.Mvvm.ComponentModel;

namespace CrazyMonkeys.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        #region ImplementacaoSemSorceCodeGeneratorMVVM_Binding
        //private bool isBusy;
        //public bool IsBusy
        //{
        //    get => isBusy; //get { return isBusy; }
        //    set
        //    {
        //        if (isBusy == value)
        //            return;

        //        isBusy = value;
        //        OnPropertyChange(nameof(isBusy));
        //        OnPropertyChange(nameof(IsNotBusy));
        //    }
        //}

        //private string title;

        //public string Title
        //{
        //    get => title;
        //    set
        //    {
        //        if (title == value)
        //            return;

        //        title = value;
        //        OnPropertyChange(nameof(title));
        //    }
        //}

        //public bool IsNotBusy => !IsBusy;

        //public event PropertyChangedEventHandler PropertyChanged;

        ////ppublic void OnPropertyChange([CallerMemberName]string name = null) => Compilador identifica a property que está chamando o método
        //public void OnPropertyChange(string name)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}
        #endregion

        [ObservableProperty]
        public string title;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy))]
        public bool isBusy;

        [ObservableProperty]
        public bool isRefreshing;

        public bool IsNotBusy => !IsBusy;
    }
}
