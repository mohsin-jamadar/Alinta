
using Alinta.Core.Service.Generic;
using Alinta.Core.Service.Abstract;
using Alinta.Core.Domain;
using Alinta.Core.Entities;
using Alinta.Core.UnitofWork;
using AutoMapper;
using System.Collections.Generic;

namespace Alinta.Core.Service
{

    public class CustomerService<Tv, Te> : GenericService<Tv, Te>, ICustomerService<Tv, Te> where Tv : CustomerViewModel
                                        where Te : Customer
    {
     

        //DI must be implemented in specific service as well beside GenericService constructor
        public CustomerService(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            
        }

        //add here any custom service method or override generic service method
 

        public IEnumerable<Tv> GetByName(string name)
        {
            var lowCaseName = name.ToLower();
            var entities = _unitOfWork.GetRepository<Te>()
                .Get(predicate: x => x.FirstName.ToLower() == lowCaseName || x.LastName.ToLower() == lowCaseName);

            return Mapper.Map<IEnumerable<Tv>>(source: entities);

        }


    }

}
