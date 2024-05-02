using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.Interfaces.Base
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        //Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAllAsync();
    }
}
