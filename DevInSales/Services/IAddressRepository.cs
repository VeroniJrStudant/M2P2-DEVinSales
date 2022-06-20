using DEVinSalesTest.DTOs;
using DEVinSalesTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.api.Services
{
    public interface IAddressRepository
    {
        public List<Address> getAddress();

        public Address getAddress(int id);

        public Task<ActionResult<AddressDTO>> getAddress(string CEP, string Street, CityStateDTO CityStateDTO);
        public void PostAddress(Address address);
    }
}
