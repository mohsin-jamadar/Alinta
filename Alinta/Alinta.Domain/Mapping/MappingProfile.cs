
using AutoMapper;
using Alinta.Core.Domain;
using Alinta.Core.Entities;

namespace TestCore.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<Customer, CustomerViewModel>();

            CreateMissingTypeMaps = true;
        }

    }





}
