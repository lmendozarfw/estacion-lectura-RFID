using MediatR;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Application.PurchaseOrders;
using RfidTraceability.Application.PurchaseOrders.Dtos;

namespace RfidTraceability.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class PurchaseOrdersController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseOrderCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return Created($"/PurchaseOrders/{id}", new { id });
    }

    [HttpGet]
    public async Task<ActionResult<List<PurchaseOrderDto>>> GetAll(CancellationToken cancellationToken)
    {
        var purchaseOrders = await sender.Send(new GetPurchaseOrdersQuery(), cancellationToken);
        return Ok(purchaseOrders);
    }
}
