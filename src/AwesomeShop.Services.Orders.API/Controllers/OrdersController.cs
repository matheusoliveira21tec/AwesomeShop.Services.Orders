using AwesomeShop.Services.Orders.Application.UseCases.AddOrder;
using AwesomeShop.Services.Orders.Application.UseCases.GetOrderById;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeShop.Services.Orders.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromServices] IAddOrderUseCase useCase, AddOrderInput input) {
        var output = await useCase.Execute(input);

        return Accepted(output);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromServices] IGetOrderByIdUseCase useCase, Guid id) {
        var output = await useCase.Execute(id);

        if (output == null) {
            return NotFound();
        }

        return Ok(output);
    }
}