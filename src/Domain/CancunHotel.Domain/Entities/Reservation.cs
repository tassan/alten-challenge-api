﻿using CancunHotel.Domain.DomainObjects;

namespace CancunHotel.Domain.Entities;

public class Reservation : Entity
{
    public Guid CustomerId { get; set; }
    public DateTimeOffset CheckInDate { get; set; }
    public DateTimeOffset CheckOutDate { get; set; }
    public int GuestsAmount { get; set; }
    
    public Customer Customer { get; set; }

    public Reservation(Customer customer)
    {
        Customer = customer;
        CustomerId = customer.Id;
    }

    public Reservation() { }   
}