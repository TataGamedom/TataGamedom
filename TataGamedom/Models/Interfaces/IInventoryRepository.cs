﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.InventoryItems;
using TataGamedom.Models.ViewModels.InventoryItems;

namespace TataGamedom.Models.Interfaces
{
    public interface IInventoryRepository
    {
        IEnumerable<InventoryVM> Search();

        IEnumerable<InventoryItemVM> Info(int? productId);
    }
}