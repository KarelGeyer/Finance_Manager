using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models.Category;

namespace AppUI.State
{
    public class StaticDataState
    {
        private List<Category> categories;
        private string successMessage;
        private string errorMessage;
        private string linkToPage;

        public List<Category> Categories
        {
            get { return categories; }
        }

        public string SuccessMessage
        {
            get { return successMessage; }
            set
            {
                successMessage = value;
                NotifyStateChanged();
            }
        }
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                NotifyStateChanged();
            }
        }

        public string LinkToPage
        {
            get { return linkToPage; }
            set
            {
                linkToPage = value;
                NotifyStateChanged();
            }
        }

        /// <summary>
        /// TODO: Needs to be replaced with fetch request and this docstring updated
        /// </summary>
        /// <returns></returns>
        public void GetCategories()
        {
            var category1 = new Category
            {
                Id = 1,
                CategoryTypeId = 1,
                Value = "Real Estate",
            };
            var category2 = new Category
            {
                Id = 2,
                CategoryTypeId = 1,
                Value = "Test",
            };
            var category3 = new Category
            {
                Id = 3,
                CategoryTypeId = 2,
                Value = "Testing",
            };
            var category4 = new Category
            {
                Id = 4,
                CategoryTypeId = 2,
                Value = "Real Estate",
            };
            var category5 = new Category
            {
                Id = 5,
                CategoryTypeId = 3,
                Value = "Real Estate",
            };
            var category6 = new Category
            {
                Id = 6,
                CategoryTypeId = 3,
                Value = "Real Estate",
            };

            categories = new List<Category> { category1, category2, category3, category4, category5, category6 };
            NotifyStateChanged();
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
