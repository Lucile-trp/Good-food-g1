using Host.Interfaces.Repository;
using Host.Interfaces.Services;
using Host.Models;
using System.Collections.Generic;

namespace Host.Services
{
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private readonly IDeliveryAddressRepository _deliveryAddressRepository;

        public DeliveryAddressService(IDeliveryAddressRepository deliveryAddressRepository)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
        }

        // GET
        public ICollection<DeliveryAddress> GetDeliveryAddresses()
        {
            return _deliveryAddressRepository.GetDeliveryAddresses();
        }

        public DeliveryAddress GetDeliveryAddressById(int deliveryAddressId)
        {
            return _deliveryAddressRepository.GetDeliveryAddressById(deliveryAddressId);
        }

        // CREATE
        public bool CreateDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            return _deliveryAddressRepository.CreateDeliveryAddress(deliveryAddress);
        }

        // UPDATE 
        public bool UpdateDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            return _deliveryAddressRepository.UpdateDeliveryAddress(deliveryAddress);
        }

        // DELETE 
        public bool DeleteDeliveryAddress(int deliveryAddressId)
        {
            var deliveryAddress = _deliveryAddressRepository.GetDeliveryAddressById(deliveryAddressId);
            if (deliveryAddress == null)
            {
                return false;
            }
            return _deliveryAddressRepository.DeleteDeliveryAddress(deliveryAddress);
        }

        // CHECK
        public bool DeliveryAddressExists(int deliveryAddressId)
        {
            return _deliveryAddressRepository.DeliveryAddressExists(deliveryAddressId);
        }
    }
}
