﻿using System;
using System.Collections.Generic;

namespace TataGamedomWebAPI.Models.EFModels;

public partial class DiscountTypeCode
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
