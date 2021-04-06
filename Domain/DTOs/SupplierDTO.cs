using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class SupplierDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }

    public class SupplierDTOInsert
    {
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
