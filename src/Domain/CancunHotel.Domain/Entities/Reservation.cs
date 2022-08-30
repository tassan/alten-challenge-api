using System.Runtime.Serialization;
using CancunHotel.Domain.DomainObjects;

namespace CancunHotel.Domain.Entities;

public class Reservation : Entity
{
    public Guid CustomerId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int GuestsAmount { get; set; }

    public int DaysReservedCount => (CheckOutDate - CheckInDate).Days;

    public Customer Customer { get; set; }

    public Reservation(Customer customer)
    {
        Customer = customer;
        CustomerId = customer.Id;
    }

    public Reservation(Guid customerId, DateTime checkInDate, DateTime checkOutDate, int guestsAmount)
    {
        CustomerId = customerId;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        GuestsAmount = guestsAmount;
    }

    public Reservation()
    {
    }
}