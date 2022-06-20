using DevInSales.DTOs;
using DevInSales.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Services

{
    public interface IAddressRepository
    {
        public List<Address> getAddress();

        public Address getAddress(int id);

        public Task<ActionResult<AddressDTO>> getAddress(string CEP, string Street, CityStateDTO CityStateDTO);
        public void PostAddress(Address address);
    }
}
