using BookStore.Context;
using BookStore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
       where TEntity : class
       where TContext : BookStoreContext
    {
       
            private readonly TContext _dbContext;

            private DbSet<TEntity> _dbSet;
            protected DbSet<TEntity> DbSet
            {
                get { return _dbSet ??= _dbContext.Set<TEntity>(); }
            }
            protected BaseRepository(TContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<List<TEntity>> GetAll()
            {
                return await _dbContext.Set<TEntity>().ToListAsync();
            }
            public async Task<TEntity> GetById(int id)
            {
                return await _dbContext.Set<TEntity>().FindAsync(id);
            }
            public async Task<TEntity> Add(TEntity entity)
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            public async Task<TEntity> Update(TEntity entity)
            {
                _dbContext.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            public async Task<bool> Delete(int id)
            {
                var entity = await _dbContext.FindAsync<TEntity>(id);
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;

            }
    }
}
