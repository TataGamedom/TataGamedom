﻿using TataGamedomWebAPI.Models.Common;

namespace TataGamedomWebAPI.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetListAsync();

    Task<T?> GetByIdAsync(int id);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

}
