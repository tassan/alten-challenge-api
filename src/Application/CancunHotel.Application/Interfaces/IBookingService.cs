using CancunHotel.Application.ViewModels;
using FluentValidation.Results;

namespace CancunHotel.Application.Interfaces;

public interface IBookingService : IDisposable
{
    Task<ValidationResult> Register(CreateBookingViewModel bookingViewModel);
    bool CheckReservationAvailability(DateTime checkIn, DateTime checkOut);
    Task<IEnumerable<ReadBookingViewModel>> GetAll();
}