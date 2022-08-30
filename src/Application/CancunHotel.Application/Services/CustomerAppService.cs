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
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerAppService(
        ICustomerRepository customerRepository,
        IMapper mapper
    )
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        ValidationResult = new ValidationResult();
    }

    public ValidationResult ValidationResult { get; set; }

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
        var validationResult = await ValidateRegisterCustomer(customer);
        
        if (!validationResult.IsValid)
            return validationResult;

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

        return validationResult;
    }

    public async Task<ValidationResult> Update(CustomerViewModel customerViewModel)
    {
        var customer = _mapper.Map<Customer>(customerViewModel);
        var validationResult = await ValidateUpdateCustomer(customer);
        
        if (!validationResult.IsValid)
            return validationResult;

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
        
        _customerRepository.Update(customer);

        return validationResult;
    }

    public Task<ValidationResult> Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    private async Task<ValidationResult> ValidateRegisterCustomer(Customer customer)
    {
        var registerCustomerValidation = new RegisterCustomerValidation();
        return await registerCustomerValidation.ValidateAsync(customer);
    }
    
    private async Task<ValidationResult> ValidateUpdateCustomer(Customer customer)
    {
        var updateCustomerValidation = new UpdateCustomerValidation();
        return await updateCustomerValidation.ValidateAsync(customer);
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