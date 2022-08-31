using CancunHotel.Application.Interfaces;
using CancunHotel.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CancunHotel.Services.API.Controllers;

[ApiController]
public class CustomerController : ApiController
{
    private readonly ICustomerAppService _customerAppService;

    public CustomerController(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;
    }

    [AllowAnonymous]
    [HttpGet("customer-management")]
    public async Task<IEnumerable<CustomerViewModel>> Get()
    {
        return await _customerAppService.GetAll();
    }

    [AllowAnonymous]
    [HttpGet("customer-management/{id:guid}")]
    public async Task<CustomerViewModel> Get(Guid id)
    {
        return await _customerAppService.GetById(id);
    }

    [HttpPost("customer-management")]
    public async Task<IActionResult> Post([FromBody] CustomerViewModel customerViewModel)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _customerAppService.Register(customerViewModel));
    }

    [HttpPut("customer-management")]
    public async Task<IActionResult> Put([FromBody] CustomerViewModel customerViewModel)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _customerAppService.Update(customerViewModel));
    }

    [HttpDelete("customer-management/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.Remove(id));
    }
}