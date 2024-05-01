using Common.Enums;
using Common.Exceptions;
using Common.Models.Income;
using Common.Models.Properties;
using Common.Response;
using LoansService.Db;
using Microsoft.AspNetCore.Mvc;

namespace PropertiesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertiesConrtoller : ControllerBase
    {
        IDbService _dbService;
        public PropertiesConrtoller(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet(Name = "Get")]
        public async Task<BaseResponse<List<Property>>> GetAll(int ownerId)
        {
            BaseResponse<List<Property>> res = new();

            try
            {
                List<Property> properties =  await _dbService.GetAll(ownerId);
                res.Data = properties; 
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

        [HttpGet(Name = "GetById")]
        public async Task<BaseResponse<Property>> GetById(int ownerId, int id)
        {
            BaseResponse<Property> res = new();

            try
            {
                Property property = await _dbService.Get(id);
                res.Data = property;
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

        [HttpGet(Name = "GetByCategory")]
        public async Task<BaseResponse<List<Property>>> GetByCategory(int ownerId, int categoryId)
        {
            BaseResponse<List<Property>> res = new();

            try
            {
                List<Property> properties = await _dbService.GetByCategory(ownerId, categoryId);
                res.Data = properties;
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

        [HttpPost(Name = "Create")]
        public async Task<BaseResponse<bool>> Create(CreateProperty createProperty)
        {
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _dbService.Create(createProperty);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch(FailedToCreateException<Property> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        [HttpPut(Name = "UpdateName")]
        public async Task<BaseResponse<bool>> UpdateName(int ownerId, string name)
        {
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _dbService.UpdateName(ownerId, name);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch(FailedToUpdateException<Property> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        [HttpPut(Name = "UpdateValue")]
        public async Task<BaseResponse<bool>> UpdateValue(int ownerId, double value)
        {
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _dbService.UpdateValue(ownerId, value);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch(FailedToUpdateException<Property> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        [HttpDelete(Name = "DeleteById")]
        public async Task<BaseResponse<bool>> DeleteById(int ownerId, int id)
        {
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _dbService.DeleteById(ownerId, id);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch(FailedToDeleteException<Property> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }

        [HttpDelete(Name = "DeleteAll")]
        public async Task<BaseResponse<bool>> DeleteAll(int ownerId)
        {
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _dbService.DeleteAll(ownerId);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch(FailedToDeleteException<Property> ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;
            }

            return res;
        }
    }
}
