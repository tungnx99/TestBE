using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Supplier : BaseCategory
    {
        public ICollection<Product> Product { get; set; }
    }
}
