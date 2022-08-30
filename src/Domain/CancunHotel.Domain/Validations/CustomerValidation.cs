using CancunHotel.Domain.Entities;
using FluentValidation;

namespace CancunHotel.Domain.Validations;

public class CustomerValidation : AbstractValidator<Customer>
{
    protected void ValidateFirstName()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("Please ensure you have entered the Name")
            .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
    }
    
    protected void ValidateLastName()
    {
        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Please ensure you have entered the Name")
            .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
    }
    
    protected void ValidateEmail()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();
    }
    
    protected void ValidateBirthDate()
    {
        RuleFor(c => c.BirthDate)
            .NotEmpty()
            .Must(HaveMinimumAge)
            .WithMessage("The customer must have 18 years or more");
    }
    
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }
    
    protected static bool HaveMinimumAge(DateOnly birthDate)
    {
        return birthDate <= DateOnly.FromDateTime(DateTime.Now.Date.AddYears(-18));
    }
}