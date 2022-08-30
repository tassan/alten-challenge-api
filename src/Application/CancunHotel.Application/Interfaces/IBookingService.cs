using System.ComponentModel.DataAnnotations;
using CancunHotel.Application.ViewModels;

namespace CancunHotel.Application.Interfaces;

public interface IBookingService : IDisposable
{
    Task<ValidationResult> Register(BookingViewModel bookingViewModel);
}