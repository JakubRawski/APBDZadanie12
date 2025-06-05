using Tutorial5.DTOs;

namespace Tutorial5.Services;

// Services/ITripService.cs
public interface ITripService
{
    Task<TravelAgency.PaginatedResponseDto<TravelAgency.TripDto>> GetTripsAsync(int page, int pageSize);
    Task<(bool Success, string Message)> AssignClientToTripAsync(int idTrip, AssignClientToTripRequest request);
    
}
