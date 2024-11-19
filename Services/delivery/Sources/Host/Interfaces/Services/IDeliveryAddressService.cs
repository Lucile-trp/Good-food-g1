using Host.Models;
using System.Collections.Generic;

namespace Host.Interfaces.Services
{
    public interface IDeliveryAddressService
    {
        ICollection<DeliveryAddress> GetDeliveryAddresses();
        DeliveryAddress GetDeliveryAddressByIdAsNoTracking(int deliveryAddressId);
        DeliveryAddress GetDeliveryAddressById(int deliveryAddressId);
        DeliveryAddress GetDeliveryAddressByOrder(int orderId);
        ICollection<DeliveryAddress> GetDeliveryAddressesByCustomer(int customerId);
        bool CreateDeliveryAddress(DeliveryAddress deliveryAddress);
        bool UpdateDeliveryAddress(DeliveryAddress deliveryAddress);
        bool DeleteDeliveryAddress(int deliveryAddressId);
        bool DeliveryAddressExists(int deliveryAddressId);
    }
}
