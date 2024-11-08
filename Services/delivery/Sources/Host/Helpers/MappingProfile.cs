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
            CreateMap<OrderDto, Order>();

            CreateMap<DeliveryAddress, DeliveryAddressDto>();
            CreateMap<DeliveryAddressDto, DeliveryAddress>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}