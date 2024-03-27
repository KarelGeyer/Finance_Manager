﻿using Common.Enums;
using Common.Models;
using Common.Models.Category;
using Common.Models.Savings;

namespace SavingsService.Service
{
    public interface IDbService
    {
        Task<float> Get(int userId);

        Task<bool> Create(int userId);

        Task<bool> Update(UpdateSavings request);

        Task<bool> Delete(int userId);

        Task GetUserId(int userId);
    }
}