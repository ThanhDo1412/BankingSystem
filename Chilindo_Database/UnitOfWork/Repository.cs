using Chilindo_Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chilindo_Data.UnitOfWork
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ChilinDoContext ChilinDoContext { get; set; }

        public Repository(ChilinDoContext chilinDoContext)
        {
            this.ChilinDoContext = chilinDoContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this.ChilinDoContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAync(Expression<Func<T, bool>> expression)
        {
            return await this.ChilinDoContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> FindByIdAync(int id)
        {
            return await this.ChilinDoContext.Set<T>().FindAsync(id);
        }

        public void Create(T entity)
        {
            this.ChilinDoContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.ChilinDoContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.ChilinDoContext.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await this.ChilinDoContext.SaveChangesAsync();
        }
    }
}
