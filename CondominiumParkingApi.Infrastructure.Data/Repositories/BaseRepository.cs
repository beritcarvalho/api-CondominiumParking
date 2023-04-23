using CondominiumParkingApi.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CondominiumParkingApi.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> where T : class
    {
        protected readonly DataBaseContext Context;

        public BaseRepository(DataBaseContext context) => Context = context;

        //Create
        public T Insert(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> InsertRangeAsync(List<T> entities)
        {
            Context.Set<T>().AddRangeAsync(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        //Read
        public T GetById(int id)
        {
            var entity = Context.Set<T>().Find(id);
            return entity;
        }

        public T GetById(decimal id)
        {
            var entity = Context.Set<T>().Find(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(decimal id)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            return entity;
        }

        public List<T> GetAll() => Context.Set<T>().ToList();

        public Task<List<T>> GetAllAsync() => Context.Set<T>().ToListAsync();

        //Update
        public T Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> UpdateAsync(List<T> entities)
        {
            Context.UpdateRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        //Delete
        public void Remove(T entidade)
        {
            Context.Set<T>().Remove(entidade);
            Context.SaveChanges();
        }

        public async Task RemoveAsync(T entidade)
        {
            Context.Set<T>().Remove(entidade);
            await Context.SaveChangesAsync();
        }
    }

}