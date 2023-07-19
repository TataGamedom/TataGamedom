﻿using System;
using System.Collections.Generic;

namespace TataGamedom_FrontEnd.Models.EFModels
{
    public partial class IssueStatusCode
    {
        public IssueStatusCode()
        {
            Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
