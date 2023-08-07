﻿namespace TataGamedomWebAPI.Application.Features.OrderItem.Queries.GetOrderItemListByAccount;

public class OrderItemWithDetailsDto
{
    public string GameGameCoverImg { get; set; } = string.Empty;

    public string GameChiName { get; set; } = string.Empty;

    public decimal DiscountedPrice { get; set; }

    public bool ProductIsVirtual { get; set; }

}