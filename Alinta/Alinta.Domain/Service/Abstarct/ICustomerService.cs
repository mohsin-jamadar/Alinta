

using System.Collections.Generic;

namespace Alinta.Core.Service.Abstract
{
    

    public interface ICustomerService<Tv, Te>  : IService<Tv, Te>
    {
        IEnumerable<Tv> GetByName(string name);

    }

  
}