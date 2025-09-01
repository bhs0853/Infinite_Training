using Question1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Question1.Controllers
{
    public class CountryController : ApiController
    {
        static List<Country> countryList = new List<Country>()
        {
            new Country{ID = 1, CountryName = "India", Capital = "New Delhi"}
        };

        [HttpGet]
        public IEnumerable<Country> GetAllCountries()
        {
            return countryList;
        }

        [HttpGet]
        public IHttpActionResult GetCountryById(int id)
        {
            Country country = countryList.Find(c => c.ID == id);
            if (country == null)
                return NotFound();
            return Ok(country);
        }

        [HttpPost]
        public IEnumerable<Country> PostCountry([FromBody] Country country)
        {
            countryList.Add(country);
            return countryList;
        }

        [HttpPut]
        public Country Put(int id, [FromBody] Country country)
        {
            var countryById = countryList.Find(c => c.ID == id);

            if (countryById == null)
                return null;

            countryById.ID = country.ID;
            countryById.CountryName = country.CountryName;
            countryById.Capital = country.Capital;
            return countryById;
        }

        [HttpDelete]
        public IEnumerable<Country> DeleteCountry(int id)
        {
            var countryById = countryList.Find(c => c.ID == id);

            if (countryById != null)
                countryList.Remove(countryById);
            return countryList;
        }
    }
}
