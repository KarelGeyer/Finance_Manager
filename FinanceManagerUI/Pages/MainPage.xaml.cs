using FinanceManagerUI.ViewModels;

namespace FinanceManagerUI.Pages
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
