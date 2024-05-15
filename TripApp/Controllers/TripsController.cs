using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripApp.Context;
using TripApp.Models;

namespace TripApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {

        private S25132Context _context;

        public TripsController(IConfiguration configuration)
        {
            _context = new S25132Context(configuration);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var result = await _context.Trips
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

        [HttpPost("{idTrip}/clients")]
        public async Task<ActionResult> AddClientToTrip(int idTrip, AddClientToTripDTO addClientToTripDTO)
        {
            var trip = await _context.Trips.FindAsync(idTrip);
            if (trip == null)
            {
                return NotFound("Trip not found.");
            }

            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == addClientToTripDTO.Pesel);
            if (existingClient == null)
            {
                existingClient = new Client
                {
                    FirstName = addClientToTripDTO.FirstName,
                    Pesel = addClientToTripDTO.Pesel
                    //...
                };
                _context.Clients.Add(existingClient);
            }


            if (trip.ClientTrips.Any(ct => ct.IdClient == existingClient.IdClient))
            {
                return BadRequest("Client is already assigned to this trip.");
            }

            var clientTrip = new ClientTrip
            {
                IdClient = existingClient.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = addClientToTripDTO.PaymentDate,
                IdClientNavigation = existingClient,
                IdTripNavigation = trip
            };

            _context.ClientTrips.Add(clientTrip);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
