using CancunHotel.Application.Interfaces;
using CancunHotel.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CancunHotel.Services.API.Controllers;

[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [AllowAnonymous]
    [HttpPost("booking-management")]
    public async Task<IActionResult> Post([FromBody] BookingViewModel bookingViewModel)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(await _bookingService.Register(bookingViewModel));
    }
    
    [AllowAnonymous]
    [HttpGet("booking-management")]
    public async Task<IActionResult> GetAll()
    {
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(await _bookingService.GetAll());
    }
}