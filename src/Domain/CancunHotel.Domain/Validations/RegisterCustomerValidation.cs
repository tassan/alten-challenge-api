namespace CancunHotel.Domain.Validations;

public class RegisterCustomerValidation : CustomerValidation
{
    public RegisterCustomerValidation()
    {
        ValidateId();
        ValidateFirstName();
        ValidateLastName();
        ValidateEmail();
    }
}