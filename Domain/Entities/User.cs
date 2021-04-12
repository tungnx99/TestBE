using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public DateTime BrithDay { get; set; }
        [Required]
        public String Gender { get; set; } // Todo: Rename to Gender
        [Required]
        public String Name { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        public int Role { get; set; } // Todo: Use specific name eg: Role

        static List<User> accounts;
        //public UserDTO MapUserDto()
        //{
        //    return new UserDTO() { UserName = UserName, Password = Password };
        //}
        public static List<User> GetList()
        {
            if (accounts == null)
            {
                accounts = new List<User>();
                for (int i = 0; i < 101; i++)
                {
                    accounts.Add(new User()
                    {
                        UserName = "admin" + (i + 1),
                        Password = "123456",
                        Id = Guid.NewGuid(),
                        Address = "aaaa",
                        BrithDay = DateTime.Now,
                        Name = "admin",
                        Gender = "Male",
                        Role = new Random().Next(0, 3)
                    });
                }
            }
            return accounts;
        }
    }
}
