using AutoMapper;
using CancunHotel.Application.Interfaces;
using CancunHotel.Application.ViewModels;
using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Handler;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Domain.Validations;
using FluentValidation.Results;

namespace CancunHotel.Application.Services;

public class BookingService : CommandHandler, IBookingService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    protected BookingService(IReservationRepository reservationRepository, ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ValidationResult> Register(BookingViewModel bookingViewModel)
    {
        var reservation = _mapper.Map<Reservation>(bookingViewModel);
        reservation.Customer = await _customerRepository.GetById(reservation.CustomerId);

        var validationResult = await ValidateBooking(reservation);

        if (!validationResult.IsValid)
            return validationResult;

        if (CheckReservationAvailability(reservation.CheckInDate, reservation.CheckOutDate))
        {
            AddError("There's already one reservation for the desired date");
            return ValidationResult;
        }
        
        _reservationRepository.Add(reservation);
        return await Commit(_reservationRepository.UnitOfWork);
    }

    public bool CheckReservationAvailability(DateTime checkIn, DateTime checkOut)
    {
        return _reservationRepository.GetByDates(checkIn, checkOut).Any();
    }

    private async Task<ValidationResult> ValidateBooking(Reservation reservation)
    {
        var bookingValidation = new BookingValidation();
        return await bookingValidation.ValidateAsync(reservation);
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