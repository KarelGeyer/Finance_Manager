using FinanceManagerUI.ViewModels;

namespace FinanceManagerUI.Pages;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage(RegistrationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
