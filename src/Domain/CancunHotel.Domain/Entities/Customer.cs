using CancunHotel.Domain.DomainObjects;

namespace CancunHotel.Domain.Entities;

public class Customer : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }

    public Customer() { }

    public Customer(string firstName, string lastName, string email, DateOnly birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
    }

    public void Delete(bool deleteUser = true) => Deleted = deleteUser;
}