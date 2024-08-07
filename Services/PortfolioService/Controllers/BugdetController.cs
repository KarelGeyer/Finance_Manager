﻿using Common.Enums;
using Common.Exceptions;
using Common.Models.PortfolioModels.Budget;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using PortfolioService.Interfaces.Services;

namespace PortfolioService.Controllers
{
    [Route("api/budget")]
    [Route("[controller]")]
    public class BugdetController : ControllerBase
    {
        private readonly ICommonService<Budget> _commonService;
        private readonly ILogger<BugdetController> _logger;

        public BugdetController(ICommonService<Budget> commonService, ILogger<BugdetController> logger)
        {
            _commonService = commonService;
            _logger = logger;
        }

        /// <summary>
        /// retrieves a list of <see cref="Budget"/> budgets
        /// </summary>
        /// <returns><see cref="Task"/> with <see cref="{T}"/> where T equals <see cref="Budget"/> budget overview</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<List<Budget>>> GetBudgets(int ownerId)
        {
            _logger.LogInformation($"{nameof(GetBudgets)} - method start");
            BaseResponse<List<Budget>> res = new();

            try
            {
                List<Budget> budgets = await _commonService.GetEntities(ownerId);
                res.Data = budgets;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is NotFoundException || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetBudgets)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetBudgets)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// retrieves a <see cref="Budget"/> budget
        /// </summary>
        /// <returns><see cref="Task"/> with <see cref="{T}"/> where T equals <see cref="Budget"/> budget</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Budget>> GetBudget(int id)
        {
            _logger.LogInformation($"{nameof(GetBudget)} - method start");
            BaseResponse<Budget> res = new();

            try
            {
                _logger.LogInformation($"{nameof(GetBudget)} - retrieving budget");
                Budget budget = await _commonService.GetEntity(id);
                res.Data = budget;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is NotFoundException || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = null;
                res.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetBudget)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(GetBudget)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Create a new budget.
        /// </summary>
        /// <param name="budgetToBeCreated">The budget creation request.</param>
        /// <returns>A boolean indicating if the creation was successful.</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> CreateBudget([FromBody] Budget budgetToBeCreated)
        {
            _logger.LogInformation($"{nameof(CreateBudget)} - method start");
            BaseResponse<bool> res = new();

            try
            {
                _logger.LogInformation($"{nameof(CreateBudget)} - creating budget");
                bool result = await _commonService.CreateEntity(budgetToBeCreated);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex) when (ex is FailedToCreateException<Budget> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = EHttpStatus.BAD_REQUEST;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(CreateBudget)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(CreateBudget)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Update the name of a budget.
        /// </summary>
        /// <param name="updateBudget">The budget update name request.</param>
        /// <returns>A boolean indicating if the update was successful.</returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> UpdateBudget([FromBody] Budget updateBudget)
        {
            _logger.LogInformation($"{nameof(UpdateBudget)} - method start");
            BaseResponse<bool> res = new();

            try
            {
                _logger.LogInformation($"{nameof(UpdateBudget)} - updating budget");
                bool result = await _commonService.UpdateEntity(updateBudget);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
                when (ex is NotFoundException || ex is FailedToUpdateException<Budget> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(UpdateBudget)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(UpdateBudget)} - {res.Status} - {ex.Message}");
            }

            return res;
        }

        /// <summary>
        /// Delete a budget.
        /// </summary>
        /// <param name="id">The budget ID.</param>
        /// <returns>A boolean indicating if the deletion was successful.</returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<bool>> DeleteBudget(int id)
        {
            _logger.LogInformation($"{nameof(DeleteBudget)} - method start");
            BaseResponse<bool> res = new();

            try
            {
                bool result = await _commonService.DeleteEntity(id);
                res.Data = result;
                res.Status = EHttpStatus.OK;
            }
            catch (Exception ex)
                when (ex is NotFoundException || ex is FailedToDeleteException<Budget> || ex is ArgumentException || ex is ArgumentNullException)
            {
                res.Data = false;
                res.Status = ex switch
                {
                    NotFoundException => EHttpStatus.NOT_FOUND,
                    _ => EHttpStatus.BAD_REQUEST
                };
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(DeleteBudget)} - {res.Status} - {ex.Message}");
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Status = EHttpStatus.INTERNAL_SERVER_ERROR;
                res.ResponseMessage = ex.Message;

                _logger.LogError($"{nameof(DeleteBudget)} - {res.Status} - {ex.Message}");
            }

            return res;
        }
    }
}
