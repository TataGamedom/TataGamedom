﻿using System;
using System.Collections.Generic;

namespace TataGamedomWebAPI.Models.EFModels;

public partial class ShipmentStatusesCode
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
