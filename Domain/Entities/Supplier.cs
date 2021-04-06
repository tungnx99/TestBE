﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Supplier : BaseCategory
    {
        public bool IsDeleted { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
