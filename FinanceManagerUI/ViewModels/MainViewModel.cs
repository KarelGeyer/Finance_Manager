using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FinanceManagerUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        string text = "Testing";

        [RelayCommand]
        void SetText()
        {
            Text = "Test";
        }
    }
}
