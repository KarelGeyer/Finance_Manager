using Common.Enums;
using Common.Models.ProductModels.Properties;
using Common.Models.UI.Property;
using Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PropertyController : Controller
    {
        HttpClient client = new HttpClient();
        PropertyVM model;

        public PropertyController()
        {
            model = new();
        }

        public async Task<IActionResult> Index()
        {
            BaseResponse<IEnumerable<Property>>? response = await GetProperties();

            if (response == null || response.Status != EHttpStatus.OK)
                return View(); // todo: handle error;

            ViewData["Properties"] = (response.Data);
            ViewData["Properties"] = (response.Data);
            return View(model);
        }

        public IActionResult Edit(int id, string name)
        {
            ViewData["modelId"] = id;
            ViewData["modelName"] = name;
            return View(model);
        }

        public IActionResult ShowDeletePartial()
        {
            model.ShouldDeletePartialBeVisible = true;
            return View(model);
        }

        public async Task<IActionResult> SubmitPropertyEdit(EditProperty property)
        {
            BaseResponse<Property>? foundProperty = await GetProperty(property.Id);
            if (foundProperty == null || foundProperty!.Data == null)
            {
                return RedirectToAction("Error");
            }

            Property updatedProperty =
                new()
                {
                    Id = property.Id,
                    CreatedAt = foundProperty.Data.CreatedAt,
                    Name = property.Name,
                    Value = property.Value,
                    CategoryId = property.CategoryId,
                    OwnerId = foundProperty.Data.OwnerId
                };

            bool updated = await EditProperty(updatedProperty);

            if (!updated)
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index");
        }

        private async Task<BaseResponse<IEnumerable<Property>>?> GetProperties()
        {
            try
            {
                var response = await client.GetAsync("https://localhost:5102/api/properties/GetAllProperties?ownerId=1");
                return await response.Content.ReadFromJsonAsync<BaseResponse<IEnumerable<Property>>>()!;
            }
            catch (Exception)
            {
                return new BaseResponse<IEnumerable<Property>>()
                {
                    ResponseMessage = "Client failed to fetch data",
                    Status = EHttpStatus.SERVICE_UNAVAILABLE
                };
            }
        }

        private async Task<BaseResponse<Property>?> GetProperty(int id)
        {
            try
            {
                var response = await client.GetAsync($"https://localhost:5102/api/properties/GetProperty?id={id}");
                return await response.Content.ReadFromJsonAsync<BaseResponse<Property>>()!;
            }
            catch (Exception)
            {
                return new BaseResponse<Property>() { ResponseMessage = "Client failed to fetch data", Status = EHttpStatus.SERVICE_UNAVAILABLE };
            }
        }

        private async Task<bool> EditProperty(Property property)
        {
            var jsonContent = JsonContent.Create(property);
            var response = await client.PutAsync("https://localhost:5102/api/properties/UpdateProperty", jsonContent);
            return true;
        }
    }
}
