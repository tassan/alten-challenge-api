using CancunHotel.Domain.Interfaces.Data;
using FluentValidation.Results;

namespace CancunHotel.Domain.Handler;

public class CommandHandler
{
    protected readonly ValidationResult ValidationResult;

    protected CommandHandler() => ValidationResult = new ValidationResult();

    protected void AddError(string message) =>
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));

    protected async Task<ValidationResult> Commit(
        IUnitOfWork uow,
        string message)
    {
        if (!await uow.CommitAsync())
            AddError(message);
        return ValidationResult;
    }

    protected async Task<ValidationResult> Commit(IUnitOfWork uow) =>
        await Commit(uow, "There was an error saving data").ConfigureAwait(false);
}