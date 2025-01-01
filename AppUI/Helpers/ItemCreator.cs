using AppUI.Helpers.Enums;
using AppUI.Services;
using Common.Models.PortfolioModels;
using Common.Models.ProductModels.Properties;
using Common.Response;

namespace AppUI.Helpers
{
    public class ItemCreator
    {
        private readonly PropertiesService _service;

        public ItemCreator(PropertiesService service)
        {
            _service = service;
        }

        public async Task<BaseResponse<bool>> CreatePortfolioModel<T>(EItemType portfolioType, T item)
            where T : PortfolioModel
        {
            BaseResponse<bool> result =
                new()
                {
                    Data = false,
                    Status = Common.Enums.EHttpStatus.INTERNAL_SERVER_ERROR,
                    ResponseMessage = "Something Went Wrong",
                };

            switch (portfolioType)
            {
                case EItemType.Property:
                {
                    result = await _service.CreateProperty(item as Property);
                    break;
                }
                default:
                    break;
            }

            return result;
        }
    }
}
