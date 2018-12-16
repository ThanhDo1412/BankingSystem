using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chilindo_Data.UnitOfWork
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAync(Expression<Func<T, bool>> expression);
        Task<T> FindByIdAync(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
