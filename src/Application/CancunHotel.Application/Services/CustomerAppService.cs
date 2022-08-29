using AutoMapper;
using CancunHotel.Application.Interfaces;
using CancunHotel.Application.ViewModels;
using CancunHotel.Domain.Entities;
using CancunHotel.Domain.Interfaces.Repository;
using CancunHotel.Domain.Validations;
using FluentValidation.Results;

namespace CancunHotel.Application.Services;

public class CustomerAppService : ICustomerAppService
{
    public ValidationResult ValidationResult { get; set; }

    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly RegisterCustomerValidation _registerCustomerValidation;

    public CustomerAppService(
        ICustomerRepository customerRepository,
        IMapper mapper,
        RegisterCustomerValidation registerCustomerValidation
    )
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _registerCustomerValidation = registerCustomerValidation;
        ValidationResult = new ValidationResult();
    }

    public async Task<IEnumerable<CustomerViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetAll());
    }

    public async Task<CustomerViewModel> GetById(Guid id)
    {
        return _mapper.Map<CustomerViewModel>(await _customerRepository.GetById(id));
    }

    public async Task<ValidationResult> Register(CustomerViewModel customerViewModel)
    {
        var customer = _mapper.Map<Customer>(customerViewModel);
        var validation = await _registerCustomerValidation.ValidateAsync(customer);
        if (!validation.IsValid)
            return validation;

        var customerExists = await _customerRepository.GetByEmail(customer.Email);
        if (customerExists != null && customerExists.Id != customer.Id)
        {
            if (!customerExists.Equals(customer))
            {
                ValidationResult.Errors.Add(new ValidationFailure("ERROR",
                    "The customer e-mail has already been taken."));
                return ValidationResult;
            }
        }
        
        _customerRepository.Add(customer);

        return validation;
    }

    public Task<ValidationResult> Update(CustomerViewModel customerViewModel)
    {
        throw new NotImplementedException();
    }

    public Task<ValidationResult> Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        Dispose();
    }
}