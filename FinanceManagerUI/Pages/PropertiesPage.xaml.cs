using FinanceManagerUI.ViewModels;

namespace FinanceManagerUI.Pages;

public partial class PropertiesPage : ContentPage
{
    public PropertiesPage(PropertiesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
