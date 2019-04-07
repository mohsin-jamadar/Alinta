using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alinta.Core.Repository;
using Alinta.Core.UnitofWork;
using Alinta.Entity.Context;
using Alinta.Entity.UnitofWork;
using Microsoft.EntityFrameworkCore;


namespace Alinta.Entity.Repository
{

    /// <summary>
    /// General repository class async
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly DbContext Context;
        public RepositoryAsync(DbContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }
        public async Task<IEnumerable<T>> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }
        public async Task Insert(T entity)
        {
            if (entity != null)
                await Context.Set<T>().AddAsync(entity);
        }
        public async Task Update(object id, T entity)
        {
            if (entity != null)
            {
                T entitytoUpdate = await Context.Set<T>().FindAsync(id);
                if (entitytoUpdate != null)
                    Context.Entry(entitytoUpdate).CurrentValues.SetValues(entity);
            }
        }
        public async Task Delete(object id)
        {
            T entity = await Context.Set<T>().FindAsync(id);
            Delete(entity);
        }
        public void Delete(T entity)
        {
            if (entity != null) Context.Set<T>().Remove(entity);
        }



    }


}
