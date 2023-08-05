﻿using Microsoft.EntityFrameworkCore;
using TataGamedomWebAPI.Application.Contracts.Persistence;
using TataGamedomWebAPI.Application.Features.InventoryItem.Queries.GetRemainingInventoryDetails;
using TataGamedomWebAPI.Infrastructure.Data;
using TataGamedomWebAPI.Models.EFModels;

namespace TataGamedomWebAPI.Infrastructure.TaTaGamedom_Persistence.Repositories;

public class InventoryItemRepository : GenericRepository<InventoryItem>, IInventoryItemRepository
{
    public InventoryItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> GetRemainingInventoryQuantity(int productId)
    {
        int inventoryItems = await _dbContext.InventoryItems
            .AsNoTracking()
            .Where(i => i.ProductId == productId)
            .CountAsync();

        int inventoryQuantitySoldOut = await _dbContext.OrderItems
            .AsNoTracking()
            .Where(o => o.ProductId == productId)
            .CountAsync();

        int remainingInventoryQuantity = inventoryItems - inventoryQuantitySoldOut;

        return remainingInventoryQuantity < 0? 0 : remainingInventoryQuantity;
    }


    public async Task<bool> IsInventoryItemExist(int inventoryItemId)
    {
        return await _dbContext.InventoryItems.AnyAsync(i => i.Id == inventoryItemId);
    }

    public async Task<bool> IsInventoryItemNotSold(int inventoryItemId)
    {
        return await _dbContext.OrderItems.AnyAsync(o => o.InventoryItemId == inventoryItemId) == false;
    }
}

