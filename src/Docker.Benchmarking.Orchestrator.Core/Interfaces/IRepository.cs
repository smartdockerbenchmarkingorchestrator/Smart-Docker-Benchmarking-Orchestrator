using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        T GetById(Guid id);

        Task<IEnumerable<T>> GetByIdsAsync(Guid[] ids);
        IEnumerable<T> GetByIds(Guid[] ids);

        Task<List<T>> ListAsync();
        List<T> List();

        Task<List<T>> ListActiveAsync();
        List<T> ListActive();

        Task<T> AddAsync(T entity);
        T Add(T entity);

        Task UpdateAsync(T entity);
        void Update(T entity);

        Task DeleteAsync(T entity);
        void Delete(T entity);

        Task<bool> DeleteByIdAsync(Guid id);
        bool DeleteById(Guid id);

        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);

        ////https://lostechies.com/jimmybogard/2009/09/03/ddd-repository-implementation-patterns/
        //IList<T> FindAll(IDictionary<string, object> propertyValuePairs);

        //T FindOne(IDictionary<string, object> propertyValuePairs);

        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
