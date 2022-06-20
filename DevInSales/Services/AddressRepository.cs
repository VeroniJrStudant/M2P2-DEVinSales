using DevInSales.Context;
using DevInSales.DTOs;
using DevInSales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Services;
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
            return _context.Address.Find(id);
        }

        public async Task<ActionResult<AddressDTO>> getAddress(string CEP, string Street, CityStateDTO CityStateDTO)
        {
            var street_find = await _context.Address.Include(x => x.City).Include(x => x.City.State).FirstOrDefaultAsync(x => x.Street == Street);
            var cep_find = await _context.Address.FindAsync(CEP);

            if (street_find == null && cep_find == null)

                return null;

            List<AddressDTO> addresses = new List<AddressDTO>();
            addresses.Add(new AddressDTO());
            return new AddressDTO
            {
                CEP = street_find.CEP,
                Street = street_find.Street,
                CityStateDTO = new CityStateDTO
                {
                    City_Id = street_find.City_Id,
                    Name_City = street_find.City.Name,
                    State_Id = street_find.City.State_Id,
                    Name_State = street_find.City.State.Name,
                    Initials = street_find.City.State.Initials
                }

            };

        }

        public void PostAddress(Address address)
        {
            _context.Address.Add(address);
            _context.SaveChanges();


        }


    }


