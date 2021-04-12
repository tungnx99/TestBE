using Common.Category;
using Common.Paganation;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Users
{
    public interface IUserService : IBasePagingService<UserDTO, UserDTO>
    {

    }
}
