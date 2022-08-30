namespace CancunHotel.Application.ViewModels;

public class ReadBookingViewModel
{
    public Guid CustomerId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int GuestsAmount { get; set; }
    public CustomerViewModel Customer { get; set; }
}