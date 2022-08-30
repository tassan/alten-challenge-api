namespace CancunHotel.Application.ViewModels;

public class CreateBookingViewModel
{
    public Guid CustomerId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int GuestsAmount { get; set; }
}