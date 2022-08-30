namespace CancunHotel.Domain.Validations;

public class BookingValidation : ReservationValidation
{
    public BookingValidation()
    {
        ValidateId();
        ValidateCheckIn();
        ValidateCheckOut();
        ValidateReservationLength();
        ValidateGuestsAmount();
        ValidateReservationCustomer();
    }
}