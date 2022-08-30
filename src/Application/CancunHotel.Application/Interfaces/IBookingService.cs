using CancunHotel.Application.ViewModels;
using FluentValidation.Results;

namespace CancunHotel.Application.Interfaces;

public interface IBookingService : IDisposable
{
    Task<ValidationResult> Register(BookingViewModel bookingViewModel);
    bool CheckReservationAvailability(DateTime checkIn, DateTime checkOut);
    Task<IEnumerable<BookingViewModel>> GetAll();
}