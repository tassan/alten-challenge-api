using CancunHotel.Domain.Core;

namespace CancunHotel.Domain.Entities;

public class Reservation : Entity
{
    public DateTimeOffset CheckInDate { get; set; }
    public DateTimeOffset CheckOutDate { get; set; }
    public Customer Customer { get; set; }
    public int AdultsAmount { get; set; }
    public int ChildrenAmount { get; set; }
    public int RoomAmount { get; set; }
    public ICollection<Room> Rooms { get; set; }
}