using kolokwium_s29754.Models;
using kolokwium_s29754.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium_s29754.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    
    
    private readonly IBookingService _Service;

    
    public BookingsController (IBookingService service)
    {
        _Service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBooking(int id)
    {

        var result = await _Service.getBooking(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);


    }

    [HttpPost]
    public async Task<IActionResult> addBooking([FromBody] addBookingDTO booking)
    {
        
        var result = await _Service.addBooking(booking);
        
        return Ok(result);
    }
    
}