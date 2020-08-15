﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;

namespace Restaurant.DAL.Repositories
{
    public class GenericRepository<TEntity, T> : IGenericRepository<TEntity, T> where TEntity : class, IEntity<T>
    {
        protected readonly RestaurantContext db;

        public GenericRepository(RestaurantContext db)
        {
            this.db = db;
        }

        public async Task Add(TEntity entity)
        {
            await db.Set<TEntity>().AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task<bool> Any(T Id)
        {
            return await db.Set<TEntity>().AnyAsync(e => e.Id.Equals(Id));
        }

        public async Task Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
            await db.SaveChangesAsync();
        }

        public async Task<TEntity> Get(T Id)
        {
            return await db.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(Id));
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await db.Set<TEntity>().ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
            await db.SaveChangesAsync();
        }
    }
}
