using Microsoft.AspNetCore.Mvc;
using Trips.Models;

namespace Trips.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly DbTripsContext _context;

        public ClientsController(DbTripsContext context)
        {
            _context = context;
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound(id);
            }

            if (client.ClientTrips.Count > 0)
            {
                return BadRequest("Client has trips. Cannot delete.");
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Accepted();
        }
    }
}
