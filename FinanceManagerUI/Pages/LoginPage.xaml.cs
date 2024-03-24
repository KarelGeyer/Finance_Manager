using FinanceManagerUI.ViewModels;

namespace FinanceManagerUI.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
