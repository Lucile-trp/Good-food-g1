using AutoMapper;
using Host.Models;
using Host.Dto;

namespace Host.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<DeliveryAddress, DeliveryAddressDto>();
        }
    }
}