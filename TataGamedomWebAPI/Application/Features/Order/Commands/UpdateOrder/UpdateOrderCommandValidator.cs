﻿using FluentValidation;
namespace TataGamedomWebAPI.Application.Features.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} 必填");
            
            RuleFor(p => p.TrackingNum)
                .NotEmpty()
                .WithMessage("{PropertyName} 必填");

            //Todo 訂單日期
        }
    }

    //Todo

    //訂單明細含實體商品 => TrackingNum必填

}