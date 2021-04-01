using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Product : BaseCategory
    {
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
