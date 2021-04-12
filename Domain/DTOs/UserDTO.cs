using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTOs
{
    public class UserDTO
    {
        public String UserName { get; set; }
        public int Role { get; set; }
    }

    public class UserLogin
    {
        public String UserName { get; set; }
        public String Password { get; set; }
    }
}
