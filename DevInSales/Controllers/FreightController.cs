﻿using DevInSales.Context;
using DevInSales.DTOs;
using DevInSales.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Controllers
{
    [Route("api/freight")]
    [ApiController]
    public class FreightController : ControllerBase
    {
        private readonly SqlContext _context;

        public FreightController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("company/{id:int}")]
        public async Task<ActionResult<ShippingCompany>> GetCompanyById(int id)
        {
            var company = await _context.ShippingCompany.FindAsync(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpGet]
        [Route("state/{stateId:int}/company/{companyId:int}")]
        public async Task<ActionResult<List<StatePrice>>> GetStateCompanyById(int stateId, int companyId)
        {

            if (!CompanyExist(companyId))
                return NotFound();

            var tabelaPreco = _context.StatePrice.Where(sp=> sp.ShippingCompanyId ==companyId && sp.StateId == stateId).ToList();

            return Ok(tabelaPreco);

        }

        private bool CompanyExist(int companyId)
        {
            return _context.StatePrice.Find(companyId) != null;

        }

        [HttpPost]
        [Route("city/company")]
        public async Task<ActionResult<List<CityPriceDTO>>> PostCityCompany(IEnumerable<CityPriceDTO> cityPrices)
        {
            if (!ExistCityAndCompany(cityPrices))
                return NotFound();

            var cityPricesEnity = GetCityPrices(cityPrices);
            _context.CityPrice.AddRange(cityPricesEnity);

            if (await _context.SaveChangesAsync() > 0)
                return Created("", cityPrices);

            return BadRequest();

        }

        private bool ExistCityAndCompany(IEnumerable<CityPriceDTO> cityPrices)
        {
            var listCompany = cityPrices.Select(sp => sp.ShippingCompanyId).Distinct();
            var listCities = cityPrices.Select(sp => sp.CityId).Distinct();
            var companiesCount = _context.ShippingCompany.Where(sc => listCompany.Contains(sc.Id)).Count();
            var citiesCount = _context.City.Where(s => listCities.Contains(s.Id)).Count();

            if (companiesCount != listCompany.Count() || citiesCount != listCities.Count())
                return false;

            return true;
        }

        private IEnumerable<CityPrice> GetCityPrices(IEnumerable<CityPriceDTO> cityPrices)
        {
            return cityPrices.Select(cp => new CityPrice
            {
                CityId = cp.CityId,
                ShippingCompanyId = cp.ShippingCompanyId,
                BasePrice = cp.BasePrice,
            });
        }

    }
}
