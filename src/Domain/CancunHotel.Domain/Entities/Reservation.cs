using CancunHotel.Domain.DomainObjects;

namespace CancunHotel.Domain.Entities;

public class Reservation : Entity
{
    public DateTimeOffset CheckInDate { get; set; }
    public DateTimeOffset CheckOutDate { get; set; }
    public Customer? Customer { get; set; }
    public int GuestsAmount { get; set; }

    public Reservation(Customer customer)
    {
        Customer = customer;
    }

    public Reservation() { }   
}