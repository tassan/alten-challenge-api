namespace CancunHotel.Domain.Validations;

public class UpdateCustomerValidation : CustomerValidation
{
    public UpdateCustomerValidation()
    {
        ValidateId();
        ValidateFirstName();
        ValidateLastName();
        ValidateEmail();
    }
}