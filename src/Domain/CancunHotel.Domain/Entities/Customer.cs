using CancunHotel.Domain.DomainObjects;

namespace CancunHotel.Domain.Entities;

public class Customer : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }

    public Customer() { }

    public Customer(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}