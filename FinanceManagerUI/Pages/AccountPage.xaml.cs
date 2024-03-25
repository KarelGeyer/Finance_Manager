using FinanceManagerUI.ViewModels;

namespace FinanceManagerUI.Pages;

public partial class AccountPage : ContentPage
{
    public AccountPage(AccountViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
