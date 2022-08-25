using CancunHotel.Domain.Core;

namespace CancunHotel.Domain.Entities;

public class Room : Entity
{
    public int BedsAmount { get; set; }
}