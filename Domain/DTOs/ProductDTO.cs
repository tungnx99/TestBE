using Domain.DTOs.BaseDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }
    }

    public class SearchProductDTO : BaseDTO
    {
        public Guid CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public Guid SupplierId { get; set; }
        public SupplierDTO Supplier { get; set; }
    }
}
