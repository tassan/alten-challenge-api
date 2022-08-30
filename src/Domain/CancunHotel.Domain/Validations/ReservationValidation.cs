using CancunHotel.Domain.Entities;
using FluentValidation;

namespace CancunHotel.Domain.Validations;

public class ReservationValidation : AbstractValidator<Reservation>
{
    protected void ValidateCheckIn()
    {
        RuleFor(r => r.CheckInDate)
            .NotEmpty()
            .WithMessage("Please ensure you have entered the Check-In Date")
            .Must(IsLessThan30Days)
            .WithMessage("The Check-In can't start in more than 30 days");
    }

    protected void ValidateCheckOut()
    {
        RuleFor(r => r.CheckOutDate)
            .NotEmpty()
            .WithMessage("Please ensure you have entered the Check-Out Date")
            .GreaterThanOrEqualTo(r => r.CheckInDate)
            .WithMessage("The Check-Out date must be greater or equal to Check-In date");
    }

    protected void ValidateGuestsAmount()
    {
        RuleFor(r => r.GuestsAmount)
            .NotEmpty()
            .WithMessage("Please ensure you have entered the Guests Amount")
            .GreaterThan(0).WithMessage("The Guests Amount must be greater than 0");
    }

    protected void ValidateReservationLength()
    {
        RuleFor(r => r.DaysReservedCount)
            .GreaterThan(0)
            .WithMessage("The reservation length must be greather than 0 Days")
            .LessThanOrEqualTo(3)
            .WithMessage("The reservation length must be less or equal to 3 Days");
    }

    protected void ValidateReservationCustomer()
    {
        RuleFor(r => r.CustomerId)
            .NotEqual(Guid.Empty);
    }

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }
    
    protected static bool IsLessThan30Days(DateTime date)
    {
        return date <= DateTime.UtcNow.AddDays(30);
    }
}