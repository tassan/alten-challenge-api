using CancunHotel.Application.Interfaces;
using CancunHotel.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CancunHotel.Services.API.Controllers;

[ApiController]
[Route("customer")]
public class CustomerController : ControllerBase
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
    public async Task<IActionResult> Post([FromBody]CustomerViewModel customerViewModel)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(await _customerAppService.Register(customerViewModel));
    }
}