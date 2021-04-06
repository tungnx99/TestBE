using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }

    public class CategoryDTOInsert
    {
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
