

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alinta.Core.Service.Abstract
{
    public interface ICustomerServiceAsync<Tv, Te>  : IServiceAsync<Tv, Te>
    {

        Task<IEnumerable<Tv>> GetByName(string name);

    }

  
}