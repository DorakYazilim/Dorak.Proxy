using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Api.Configuration;
using Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly SqlConfiguration _configuration;

        public CountryController(IOptions<SqlConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }


        [HttpGet("/api/countries")]
        public async Task<ActionResult> GetCountries()
        {
            return await Task.Run(() =>
            {
                using (var connection = new SqlConnection(_configuration.DefaultConnection))
                {
                    var query = "select * from country";
                    // execute sql query.....


                    // sample result
                    var result = new List<Country>
                    {
                        new Country(1, "Turkiye"),
                        new Country(2, "Greece"),
                        new Country(3, "Italy"),
                    };

                    return Ok(result);
                }
            });
        }
    }
}