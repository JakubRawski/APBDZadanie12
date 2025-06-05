using Microsoft.AspNetCore.Mvc;
using Tutorial5.DTOs;
using Tutorial5.Services;

namespace Tutorial5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

        
    [HttpGet] 
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and page Size must be positive integers.");
        }

        var response = await _tripService.GetTripsAsync(page, pageSize);

        if (response.Trips.Count == 0 && response.AllPages > 0 && page > response.AllPages)
        {
            return NotFound($"Page {page} does not exist");
        }

        return Ok(response);
    }

        
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientToTripRequest request)
    {
        var (success, message) = await _tripService.AssignClientToTripAsync(idTrip, request);

        if (!success)
        {
            if (message.Contains("not found"))
            {
                return NotFound(message);
            }
            if (message.Contains("already exist"))
            {
                return Conflict(message);
            }
            return BadRequest(message);
        }

        return StatusCode(201, message);
    }
}

