﻿using System;
using System.Collections.Generic;

namespace TataGamedomWebAPI.Models.EFModels;

public partial class ProductStatusCode
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
