using System.ComponentModel.DataAnnotations;
using CancunHotel.Application.Interfaces;
using CancunHotel.Application.ViewModels;

namespace CancunHotel.Application.Services;

public class BookingService : IBookingService
{
    public Task<ValidationResult> Register(BookingViewModel bookingViewModel)
    {
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        Dispose();
    }
}