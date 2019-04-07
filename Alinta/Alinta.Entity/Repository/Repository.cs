using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alinta.Core.Repository;
using Alinta.Core.UnitofWork;
using Alinta.Entity.Context;
using Alinta.Entity.UnitofWork;
using Microsoft.EntityFrameworkCore;


namespace Alinta.Entity.Repository
{

    /// <summary>
    /// General repository class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            this.Context = context;
        }


        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).AsEnumerable<T>();
        }
        public T GetOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).FirstOrDefault();
        }
        public void Insert(T entity)
        {
            if (entity != null) Context.Set<T>().Add(entity);
        }
        public void Update(object id, T entity)
        {
            if (entity != null)
            {
                T entitytoUpdate = Context.Set<T>().Find(id);
                if (entitytoUpdate != null)
                    Context.Entry(entitytoUpdate).CurrentValues.SetValues(entity);
            }
        }
        public void Delete(object id)
        {
            T entity = Context.Set<T>().Find(id);
            Delete(entity);
        }
        public void Delete(T entity)
        {
            if (entity != null) Context.Set<T>().Remove(entity);
        }

    }


}
