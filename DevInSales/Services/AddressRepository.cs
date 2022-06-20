using DEVinSalesTest.Context;
using DEVinSalesTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEVinSalesTest.api.Services
{
    public class AddressRepository : IAddressRepository
    {
        private SqlContext _context;
        public AddressRepository(SqlContext context)
        {
            _context = context;
        }
        public List<Address> getAddress()
        {
            return _context.Address.ToList();
        }

        public Address getAddress(int id)
        {
            throw new NotImplementedException();
        }

        public void PostAddress(Address address)
        {
            _context.Address.Add(address);
            _context.SaveChanges();


        }


    }
}
