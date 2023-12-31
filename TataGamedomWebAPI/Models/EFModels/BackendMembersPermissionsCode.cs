﻿using System;
using System.Collections.Generic;

namespace TataGamedomWebAPI.Models.EFModels;

public partial class BackendMembersPermissionsCode
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BackendMembersRolePermission> BackendMembersRolePermissions { get; set; } = new List<BackendMembersRolePermission>();
}
