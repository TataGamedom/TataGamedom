﻿using AutoMapper;
using MediatR;
using TataGamedomWebAPI.Application.Contracts.Logging;
using TataGamedomWebAPI.Application.Contracts.Persistence;
using TataGamedomWebAPI.Application.Exceptions;
using TataGamedomWebAPI.Application.Features.Order.Commands.CreateOrder;
using TataGamedomWebAPI.Models.EFModels;

namespace TataGamedomWebAPI.Application.Features.Order.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IAppLogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(
        IMapper mapper, 
        IOrderRepository orderRepository, 
        IAppLogger<UpdateOrderCommandHandler> logger)
    {
        this._mapper = mapper;
        this._orderRepository = orderRepository;
        this._logger = logger;
    }


    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request);

        var orderTobeUpdated = await _orderRepository.GetByIdAsync(request.Id);
        orderTobeUpdated = _mapper.Map(request, orderTobeUpdated);
        await _orderRepository.UpdateAsync(orderTobeUpdated);
        _logger.LogInformation("Order were updated successfully");

        return Unit.Value;
    }

    private static async Task ValidateRequest(UpdateOrderCommand request)
    {
        var validator = new UpdateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid order request", validationResult);
        }
    }
}