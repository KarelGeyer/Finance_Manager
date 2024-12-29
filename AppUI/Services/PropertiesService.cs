using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Common;
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
                var response = await http.PutAsJsonAsync<Property>("https://localhost:5102/api/properties/UpdateProperty", updateProperty);
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
