
using Alinta.Core.Service.Generic;
using Alinta.Core.Domain;
using Alinta.Core.Entities;
using Alinta.Core.UnitofWork;
using Alinta.Core.Service.Abstract;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Alinta.Core.Service
{

    public class CustomerServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>, ICustomerServiceAsync<Tv, Te>
                                        where Tv : CustomerViewModel
                                        where Te : Customer
    {
        //DI must be implemented specific service as well beside GenericAsyncService constructor
        public CustomerServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tv>> GetByName(string name)
        {
            var lowCaseName = name.ToLower();
            var entities = await _unitOfWork.GetRepositoryAsync<Te>()
                .Get(predicate: x => x.FirstName.ToLower() == lowCaseName || x.LastName.ToLower() == lowCaseName);

            return Mapper.Map<IEnumerable<Tv>>(source: entities);

        }

    }

}
