using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class BaseCategory : BaseEntity
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
    }
}
