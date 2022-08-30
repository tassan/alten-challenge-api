using CancunHotel.Application.ViewModels;
using FluentValidation.Results;

namespace CancunHotel.Application.Interfaces;

public interface IBookingService : IDisposable
{
    Task<ValidationResult> Register(CreateBookingViewModel bookingViewModel);
    Task<ReadBookingViewModel?> GetReservationByEmail(string email);
    bool CheckReservationAvailability(DateTime checkIn, DateTime checkOut);
    Task<IEnumerable<ReadBookingViewModel>> GetAll();
    Task<ValidationResult> Update(UpdateBookingViewModel bookingViewModel);
    Task<ValidationResult> Remove(Guid id);
}