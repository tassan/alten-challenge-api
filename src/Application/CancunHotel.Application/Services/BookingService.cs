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

    public BookingService(IReservationRepository reservationRepository, ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ValidationResult> Register(CreateBookingViewModel bookingViewModel)
    {
        var reservation = _mapper.Map<Reservation>(bookingViewModel);
        reservation.Customer = await _customerRepository.GetById(reservation.CustomerId);

        var validationResult = await ValidateRegisterBooking(reservation);

        if (!validationResult.IsValid)
            return validationResult;

        NormalizeCheckInTime(reservation);
        NormalizeCheckOutTime(reservation);

        if (!CheckReservationAvailability(reservation.CheckInDate, reservation.CheckOutDate))
        {
            AddError("There's already one reservation for the desired date");
            return ValidationResult;
        }

        _reservationRepository.Add(reservation);
        return await Commit(_reservationRepository.UnitOfWork);
    }

    public async Task<ReadBookingViewModel?> GetReservationByEmail(string email)
    {
        var readBookingViewModel = (await GetAll())
            .FirstOrDefault(r => r.Customer.Email == email);

        return readBookingViewModel;
    }

    public bool CheckReservationAvailability(DateTime checkIn, DateTime checkOut) => !_reservationRepository
        .GetByDates(checkIn.ToUniversalTime(), checkOut.ToUniversalTime()).Any();

    public async Task<IEnumerable<ReadBookingViewModel>> GetAll()
    {
        var reservations = await _reservationRepository.GetAll();

        foreach (var reservation in reservations)
            reservation.Customer = await _customerRepository.GetById(reservation.CustomerId);

        return _mapper.Map<IEnumerable<ReadBookingViewModel>>(reservations);
    }

    public async Task<ValidationResult> Update(UpdateBookingViewModel bookingViewModel)
    {
        var reservation = _mapper.Map<Reservation>(bookingViewModel);
        reservation.Customer = await _customerRepository.GetById(reservation.CustomerId);
        var validationResult = await ValidateUpdateBooking(reservation);

        if (!validationResult.IsValid)
            return validationResult;

        NormalizeCheckInTime(reservation);
        NormalizeCheckOutTime(reservation);

        _reservationRepository.Update(reservation);
        return await Commit(_reservationRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Remove(Guid id)
    {
        var reservation = await _reservationRepository.GetById(id);
        reservation.Customer = await _customerRepository.GetById(reservation.CustomerId);

        if (reservation is null)
        {
            AddError("The reservation doesn't exists.");
            return ValidationResult;
        }

        _reservationRepository.Remove(reservation);

        return await Commit(_reservationRepository.UnitOfWork);
    }

    private async Task<ValidationResult> ValidateRegisterBooking(Reservation reservation)
    {
        var bookingValidation = new BookingValidation();
        return await bookingValidation.ValidateAsync(reservation);
    }

    private async Task<ValidationResult> ValidateUpdateBooking(Reservation reservation)
    {
        var bookingValidation = new BookingValidation();
        return await bookingValidation.ValidateAsync(reservation);
    }

    private void NormalizeCheckInTime(Reservation reservation)
    {
        var checkInDate = reservation.CheckInDate;
        reservation.CheckInDate = new DateTime(checkInDate.Year, checkInDate.Month, checkInDate.Day, 0, 0, 0);
    }

    private void NormalizeCheckOutTime(Reservation reservation)
    {
        var checkOutDate = reservation.CheckOutDate;
        reservation.CheckOutDate = new DateTime(checkOutDate.Year, checkOutDate.Month, checkOutDate.Day, 23, 59, 59);
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