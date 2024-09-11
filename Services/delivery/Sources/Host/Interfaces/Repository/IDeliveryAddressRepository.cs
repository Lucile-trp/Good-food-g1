using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Repository
{
    public interface IDeliveryAddressRepository
    {
        ICollection<DeliveryAddress> GetDeliveryAddresses();
        DeliveryAddress GetDeliveryAddressById(int deliveryAddressId);
        bool CreateDeliveryAddress(DeliveryAddress deliveryAddress);
        bool UpdateDeliveryAddress(DeliveryAddress deliveryAddress);
        bool DeleteDeliveryAddress(DeliveryAddress deliveryAddress);
        bool DeliveryAddressExists(int deliveryAddressId);
        bool Save();
    }
}
