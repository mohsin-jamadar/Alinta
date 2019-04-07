using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alinta.Core.Repository;



namespace Alinta.Core.UnitofWork
{


    public interface IUnitOfWork
    {

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;

       
        bool Save();
        Task<bool> SaveAsync();
    }

   

}
