using AppUI.Helpers.Enums;
using AppUI.Services;
using Common.Models.Category;
using Common.Response;

namespace AppUI.State
{
    public class StaticDataState : BaseState
    {
        private List<Category>? _categories;
        private EResult _resultState;
        private string? _successMessage;
        private string? _errorMessage;

        public List<Category> Categories
        {
            get { return _categories!; }
        }

        public string SuccessMessage
        {
            get { return _successMessage!; }
            set
            {
                _successMessage = value;
                NotifyStateChanged();
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage!; }
            set
            {
                _errorMessage = value;
                NotifyStateChanged();
            }
        }

        public EResult ResultState
        {
            get { return _resultState; }
            set
            {
                _resultState = value;
                NotifyStateChanged();
            }
        }

        public void SetCategories(List<Category> categories)
        {
            _categories = categories;
            NotifyStateChanged();
        }
    }
}
