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
    [HttpGet("booking-management")]
    public async Task<IActionResult> GetAll()
    {
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(await _bookingService.GetAll());
    }
    
    [AllowAnonymous]
    [HttpPost("booking-management")]
    public async Task<IActionResult> Post([FromBody] CreateBookingViewModel bookingViewModel)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(await _bookingService.Register(bookingViewModel));
    }
    
    [AllowAnonymous]
    [HttpPut("booking-management")]
    public async Task<IActionResult> Put([FromBody] UpdateBookingViewModel bookingViewModel)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(await _bookingService.Update(bookingViewModel));
    }
    
    [AllowAnonymous]
    [HttpDelete("booking-management/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(await _bookingService.Remove(id));
    }

    [AllowAnonymous]
    [HttpGet("check-reservation/{email}")]
    public async Task<IActionResult> CheckReservation(string email)
    {
        var reservation = await _bookingService.GetReservationByEmail(email);

        if (reservation is null) return NotFound("We couldn't find any reservation for the entered user e-mail");

        return Ok(reservation);
    }

    [AllowAnonymous]
    [HttpPost("check-availability")]
    public IActionResult CheckAvailability([FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut)
    {
        var isAvailable = _bookingService.CheckReservationAvailability(checkIn, checkOut);

        return Ok(isAvailable
            ? $"There's already one reservation for the desired date of {checkIn} to {checkOut}"
            : $"The desired date of {checkIn} to {checkOut} is available to reservation");
    }
}