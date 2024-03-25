using FinanceManagerUI.ViewModels;

namespace FinanceManagerUI.Pages;

public partial class ExpensesPage : ContentPage
{
    public ExpensesPage(ExpensesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
