using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Category : BaseCategory
    {
        public ICollection<Product> Product { get; set; }
    }
}
