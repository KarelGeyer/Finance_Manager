using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Category;
using Common.Enums;
using Common.Request;
using Common.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postgrest.Responses;
using Supabase;
using Websocket.Client;

namespace UsersService.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Client _supabaseClient;

        public CategoryController(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Category>>> GetAll()
        {
            BaseResponse<List<Category>> res = new();

            try
            {
                var response = await _supabaseClient.From<Category>().Get();
                var categories = response.Models;

                if (categories == null || categories.Count < 1)
                {
                    res.Data = null;
                    res.Status = EHttpStatus.NOT_FOUND;
                    res.ResponseMessage = CustomResponseMessage.GetNotFoundResponseMessage(
                        nameof(Category)
                    );
                }

                res.Data = categories;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Category>>> GetByType(
            [FromBody] BaseRequest<ECategoryType> req
        )
        {
            BaseResponse<List<Category>> res = new();

            try
            {
                var response = await _supabaseClient
                    .From<Category>()
                    .Where(x => x.Type == req.Data)
                    .Get();

                var categories = response.Models;

                if (categories == null || categories.Count < 1)
                {
                    res.Data = null;
                    res.Status = EHttpStatus.NOT_FOUND;
                    res.ResponseMessage = CustomResponseMessage.GetNotFoundResponseMessage(
                        nameof(Category)
                    );
                }

                res.Data = categories;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Category>> GetById([FromBody] BaseRequest<int> req)
        {
            BaseResponse<Category> res = new();

            try
            {
                var category = await _supabaseClient
                    .From<Category>()
                    .Where(x => x.Id == req.Data)
                    .Single();

                if (category == null)
                {
                    res.Data = null;
                    res.Status = EHttpStatus.NOT_FOUND;
                    res.ResponseMessage = CustomResponseMessage.GetNotFoundResponseMessage(
                        nameof(Category)
                    );
                }

                res.Data = category;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }
    }
}
