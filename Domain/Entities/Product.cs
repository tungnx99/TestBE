using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Product : BaseCategory
    {
        public Guid? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
