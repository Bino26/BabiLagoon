using BabiLagoon.Application.Common.Interfaces.Base;
using BabiLagoon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Infrastructure.Repositories.Base
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public  async Task<T> DeleteAllAsync()
        {
            dbContext.Set<T>().RemoveRange();
            await dbContext.SaveChangesAsync();
            return null;


        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                dbContext.Set<T>().Remove(entity);
                await dbContext.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        //public async Task<T> GetByIdAsync<T>(int id) where T : class
        //{
        //    return await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        //}

    }
}
