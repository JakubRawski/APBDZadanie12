using Microsoft.EntityFrameworkCore;
using Tutorial5.DTOs;
using Tutorial5.Models;

namespace Tutorial5.Services;

public class TripService : ITripService
    {
        private readonly TravelAgencyContext _context;

        public TripService(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<TravelAgency.PaginatedResponseDto<TravelAgency.TripDto>> GetTripsAsync(int page, int pageSize)
        {
            var totalTrips = await _context.Trips.CountAsync();
            var totalPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

            if (page > totalPages && totalTrips > 0)
            {
                return new TravelAgency.PaginatedResponseDto<TravelAgency.TripDto>
                {
                    PageNum = page,
                    PageSize = pageSize,
                    AllPages = totalPages,
                    Trips = new List<TravelAgency.TripDto>()
                };
            }

            var trips = await _context.Trips
                .OrderByDescending(t => t.DateFrom)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TravelAgency.TripDto
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,
                    Countries = t.CountryTrips.Select(ct => new TravelAgency.CountryDto { Name = ct.IdCountryNavigation.Name }).ToList(),
                    Clients = t.ClientTrips.Select(ct => new TravelAgency.ClientDto { FirstName = ct.IdClientNavigation.FirstName, LastName = ct.IdClientNavigation.LastName }).ToList()
                })
                .ToListAsync();

            return new TravelAgency.PaginatedResponseDto<TravelAgency.TripDto>
            {
                PageNum = page,
                PageSize = pageSize,
                AllPages = totalPages,
                Trips = trips
            };
        }

        public async Task<(bool Success, string Message)> AssignClientToTripAsync(int idTrip, AssignClientToTripRequest request)
        {
            var trip = await _context.Trips.FindAsync(idTrip);
            if (trip == null)
            {
                return (false, $"Trip with ID {idTrip} not found.");
            }

            if (trip.DateFrom <= DateTime.Now)
            {
                return (false, "Cannot register for a trip that has already started.");
            }

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == request.Pesel);

            if (client == null)
            {
                client = new Client
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Telephone = request.Telephone,
                    Pesel = request.Pesel
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
            }
            else
            {
                var isClientAssigned = await _context.ClientTrips
                    .AnyAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == idTrip);

                if (isClientAssigned)
                {
                    return (false, "Client is already registered for this trip.");
                }
            }

            var clientTrip = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = request.PaymentDate
            };

            _context.ClientTrips.Add(clientTrip);
            await _context.SaveChangesAsync();

            return (true, "Client successfully assigned to the trip.");
        }
    }