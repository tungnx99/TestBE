using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }
    }

    public class ProductDTOInsert
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }
    }

    public class SearchProductDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public Guid SupplierId { get; set; }
        public SupplierDTO Supplier { get; set; }
    }
}
