using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using TripApp.Context;
using TripApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TripApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {

        private IConfiguration _configuration;

        public TripsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var dbContext = new S25132Context(_configuration);
            var result = await dbContext.Trips
                .OrderByDescending(t => t.DateFrom)
            .Select(t => new TripDTO
            {
                Name = t.Name,

                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                Clients = t.ClientTrips.Select(ct => new ClientDTO
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName,
                }).ToList(),
                Countries = t.IdCountries.Select(ct => new CountryDTO
                {
                    Name = ct.Name
                }).ToList()
            }).ToListAsync();

            return Ok(result);
        }

    }
}
