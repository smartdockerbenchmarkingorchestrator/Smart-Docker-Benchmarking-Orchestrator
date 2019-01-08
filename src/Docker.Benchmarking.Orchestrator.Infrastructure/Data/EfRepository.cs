using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;

        public EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbContext.AddRange(entities);
            _dbContext.SaveChanges();
        }


        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public virtual T GetById(Guid id)
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public async Task<List<T>> ListAsync()
        {
            //return await _dbContext.Set<T>().ToListAsync();

            return await _dbContext.Set<T>().ToListAsync();
        }

        public List<T> List()
        {
            return _dbContext.Set<T>().ToList();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        //http://www.tugberkugurlu.com/archive/generic-repository-pattern-entity-framework-asp-net-mvc-and-unit-testing-triangle
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _dbContext.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);
            return query;
        }

        public async Task<List<T>> ListActiveAsync()
        {
            return await _dbContext.Set<T>().Where(c=> c.Active).ToListAsync();
        }

        public List<T> ListActive()
        {
            return _dbContext.Set<T>().Where(c => c.Active).ToList();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            _dbContext.Set<T>().Remove(entity);

            var result = await _dbContext.SaveChangesAsync();

            return await Task.FromResult(result > 0);
        }

        public bool DeleteById(Guid id)
        {
            var entity = _dbContext.Set<T>().Find(id);

            _dbContext.Set<T>().Remove(entity);

            var result = _dbContext.SaveChanges();

            return Task.FromResult(result > 0).Result;
        }

        public async Task<IEnumerable<T>> GetByIdsAsync(Guid[] ids)
        {
            return await _dbContext.Set<T>().Where(c=> ids.Contains(c.Id)).ToListAsync();
        }

        public IEnumerable<T> GetByIds(Guid[] ids)
        {
            return _dbContext.Set<T>().Where(c => ids.Contains(c.Id)).ToList();
        }
    }
}
