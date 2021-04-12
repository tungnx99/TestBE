using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Auth
{
    public interface IAuthService
    {
        public string Login(UserLogin data);
    }
}
