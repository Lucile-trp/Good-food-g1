using Host.Data;
using Host.Interfaces.Repository;
using Host.Models;
using System.Collections.Generic;
using System.Linq;

namespace Host.Repository
{
    public class DeliveryAddressRepository : IDeliveryAddressRepository
    {
        private readonly DeliveryDbContext _context;

        public DeliveryAddressRepository(DeliveryDbContext context)
        {
            _context = context;
        }

        // GET 
        public ICollection<DeliveryAddress> GetDeliveryAddresses()
        {
            return _context.DeliveryAddresses.ToList();
        }

        public DeliveryAddress GetDeliveryAddressById(int deliveryAddressId)
        {
            return _context.DeliveryAddresses.FirstOrDefault(da => da.DeliveryAddressId == deliveryAddressId);
        }

        // CREATE 
        public bool CreateDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            _context.Add(deliveryAddress);
            return Save();
        }

        // UPDATE
        public bool UpdateDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            _context.Update(deliveryAddress);
            return Save();
        }

        // DELETE 
        public bool DeleteDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            _context.Remove(deliveryAddress);
            return Save();
        }

        // CHECK
        public bool DeliveryAddressExists(int deliveryAddressId)
        {
            return _context.DeliveryAddresses.Any(da => da.DeliveryAddressId == deliveryAddressId);
        }

        // SAVE
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
