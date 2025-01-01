using AppUI.Helpers.ViewModels;

namespace AppUI.State
{
    public class RouterState : BaseState
    {
        private string? _pageName = "Test";
        private string? _sectionName = "General Overview";
        private RouterParams _params = new() { LinkToPage = "", LinkToSection = "" };

        private readonly List<string> financeSubPageNames = ["General Overview", "Majetek", "Náklady a dluhy", "Příjmy a úspory", "Rozpočty"];
        private readonly List<PageData> pageNames =
            new()
            {
                new PageData { Name = "Finance", Href = "finances" },
                new PageData { Name = "Nákupní seznam", Href = "shopping-list" },
                new PageData { Name = "Recepty", Href = "receips" },
                new PageData { Name = "Počasí", Href = "weather" },
                new PageData { Name = "Účet", Href = "account" },
            };

        public string PageName
        {
            get => _pageName;
            set
            {
                _pageName = value;
                NotifyStateChanged();
            }
        }

        public string SectionName
        {
            get => _sectionName;
            set
            {
                _sectionName = value;
                NotifyStateChanged();
            }
        }

        public RouterParams Params
        {
            get { return _params; }
            set
            {
                _params = value;
                NotifyStateChanged();
            }
        }

        public List<string> FinanceSubPageNames
        {
            get => financeSubPageNames;
        }

        public List<PageData> PageNames
        {
            get => pageNames;
        }
    }
}
