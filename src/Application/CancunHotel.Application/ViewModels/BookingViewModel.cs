namespace CancunHotel.Application.ViewModels;

public class BookingViewModel
{
    public Guid CustomerId { get; set; }
    public DateTimeOffset CheckInDate { get; set; }
    public DateTimeOffset CheckOutDate { get; set; }
    public int GuestsAmount { get; set; }
}