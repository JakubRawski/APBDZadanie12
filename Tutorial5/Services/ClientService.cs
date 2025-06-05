using Microsoft.EntityFrameworkCore;
using Tutorial5.Models;

namespace Tutorial5.Services;

public class ClientService : IClientService
{
    private readonly TravelAgencyContext _context;

    public ClientService(TravelAgencyContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null)
        {
            return false;
        }

        if (client.ClientTrips.Any())
        {
            throw new InvalidOperationException("Client cannot be deleted because they are assigned to the trip.");
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }
}
