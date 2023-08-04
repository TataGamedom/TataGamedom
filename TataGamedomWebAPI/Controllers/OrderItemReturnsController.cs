﻿using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TataGamedomWebAPI.Application.Features.OrderItem.Queries.GetOrderItemDetails;
using TataGamedomWebAPI.Application.Features.OrderItemReturn.Commands.CreateOrderItemReturn;
using TataGamedomWebAPI.Application.Features.OrderItemReturn.Commands.DeleteOrderItemReturn;
using TataGamedomWebAPI.Application.Features.OrderItemReturn.Commands.UpdateOrderItemReturn;
using TataGamedomWebAPI.Application.Features.OrderItemReturn.Queries.GetOrderItemReturnList;

namespace TataGamedomWebAPI.Controllers;

[EnableCors("AllowAny")]
[Route("api/[controller]")]
[ApiController]
public class OrderItemReturnsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemReturnsController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    public async Task<List<OrderItemReturnDto>> Get()
    {
        var orderItemReturn = await _mediator.Send(new GetOrderItemReturnListQuery());
        return orderItemReturn;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItemReturnDto>> Get(int id)
    {
        var orderItemReturns = await _mediator.Send(new GetOrderItemDetailsQuery(id));
        return Ok(orderItemReturns);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateOrderItemReturnCommand orderItemReturn)
    {
        var response = await _mediator.Send(orderItemReturn);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateOrderItemReturnCommand orderItemReturn)
    {
        await _mediator.Send(orderItemReturn);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteOrderItemReturnCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}