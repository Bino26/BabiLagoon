using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.Interfaces.Base
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> DeleteAsync(int id);
        Task<T> DeleteAllAsync();
    }
}
