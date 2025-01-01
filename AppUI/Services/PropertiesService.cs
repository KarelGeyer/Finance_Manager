using System.Net.Http.Json;
using Common.Models.ProductModels.Properties;
using Common.Response;

namespace AppUI.Services
{
    public class PropertiesService(HttpClient http)
    {
        public async Task<BaseResponse<List<Property>>> GetPropertiesAsync()
        {
            try
            {
                var response = await http.GetFromJsonAsync<BaseResponse<List<Property>>>(
                    "https://localhost:5102/api/properties/GetAllProperties?ownerId=1"
                );
                return response!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> UpdateProperty(Property updateProperty)
        {
            try
            {
                var response = await http.PutAsJsonAsync("https://localhost:5102/api/properties/UpdateProperty", updateProperty);
                var res = response.Content.ReadFromJsonAsync<BaseResponse<bool>>();
                return res.Result!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> DeleteProperty(int id)
        {
            try
            {
                var response = await http.DeleteFromJsonAsync<BaseResponse<bool>>($"https://localhost:5102/api/properties/DeleteProperty?id={id}");
                return response!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> CreateProperty(Property property)
        {
            try
            {
                var response = await http.PostAsJsonAsync("https://localhost:5102/api/properties/CreateProperty", property);
                var res = response.Content.ReadFromJsonAsync<BaseResponse<bool>>();
                return res.Result!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
