using CancunHotel.Domain.Core;

namespace CancunHotel.Domain.Entities;

public class Customer : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}