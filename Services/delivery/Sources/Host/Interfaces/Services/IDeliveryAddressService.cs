using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Services
{
    public interface IDeliveryAddressService
    {
        ICollection<DeliveryAddress> GetDeliveryAddresses();
        DeliveryAddress GetDeliveryAddressById(int deliveryAddressId);
        bool CreateDeliveryAddress(DeliveryAddress deliveryAddress);
        bool UpdateDeliveryAddress(DeliveryAddress deliveryAddress);
        bool DeleteDeliveryAddress(int deliveryAddressId);
        bool DeliveryAddressExists(int deliveryAddressId);
    }
}
