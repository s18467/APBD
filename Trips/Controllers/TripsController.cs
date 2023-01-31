using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trips.Models;
using Trips.Models.Dtos;

namespace Trips.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly DbTripsContext _context;

        public TripsController(DbTripsContext context)
        {
            _context = context;
        }

        // GET: api/trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
            var list = await _context.Trips.OrderByDescending(trip => trip.DateFrom).ToListAsync();
            return Ok(list);
        }

        // GET: api/trips/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
            var trip = await _context.Trips.FindAsync(id);

            if (trip == null)
            {
                return NotFound(id);
            }

            return Ok(trip);
        }

        // POST: api/trips/{idTrip}/clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{idTrip}/clients")]
        public async Task<ActionResult<Trip>> PostTrip(int idTrip, [FromBody] NewClientTripDto tripDto)
        {
            if (idTrip < 1)
            {
                return BadRequest("Invalid trip id");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idTrip != tripDto.IdTrip)
            {
                return BadRequest("Trip id in url and in body do not match");
            }

            var trip = await _context.Trips.FindAsync(tripDto.IdTrip);
            if (trip == null)
            {
                return NotFound($"Trip {tripDto.IdTrip} does not exist");
            }
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == tripDto.Pesel);
            if (client == null)
            {
                client = new Client
                {
                    FirstName = tripDto.FirstName,
                    LastName = tripDto.LastName,
                    Email = tripDto.Email,
                    Telephone = tripDto.Telephone,
                    Pesel = tripDto.Pesel
                };
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (await _context.ClientTrips.AnyAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == tripDto.IdTrip))
                {
                    return BadRequest("Client already has this trip");
                }
            }
            var clientTrip = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = tripDto.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = tripDto.PaymentDate
            };
            await _context.ClientTrips.AddAsync(clientTrip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrip", new { id = trip.IdTrip }, trip);
        }
    }
}
