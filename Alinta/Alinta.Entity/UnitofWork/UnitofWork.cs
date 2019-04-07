using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alinta.Core.Repository;
using Alinta.Core.UnitofWork;
using Alinta.Entity.Context;
using Alinta.Entity.Repository;
using Microsoft.EntityFrameworkCore;

namespace Alinta.Entity.UnitofWork
{

    public class UnitOfWork : IUnitOfWork
    {
        public AlintaContext Context { get; }

        private Dictionary<Type, object> _repositoriesAsync;
        private Dictionary<Type, object> _repositories;
       

        public UnitOfWork(AlintaContext context)
        {
            Context = context;
           
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context);
            return (IRepository<TEntity>)_repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositoriesAsync = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositoriesAsync.ContainsKey(type)) _repositoriesAsync[type] = new RepositoryAsync<TEntity>(Context);
            return (IRepositoryAsync<TEntity>)_repositoriesAsync[type];
        }

        public bool Save()
        {
            return Context.SaveChanges() > 0;
        }
        public async Task<bool> SaveAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }

    }
}
