using System.Net.Http.Json;
using Common.Models.Category;
using Common.Response;

namespace AppUI.Services
{
    public class StaticDataService(HttpClient http)
    {
        public async Task<BaseResponse<List<Category>>> GetCategoriesAsync()
        {
            try
            {
                var response = await http.GetFromJsonAsync<BaseResponse<List<Category>>>("https://localhost:5103/api/category/GetAllCategories");
                return response!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
