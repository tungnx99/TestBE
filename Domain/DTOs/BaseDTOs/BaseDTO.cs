using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.BaseDTOs
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
