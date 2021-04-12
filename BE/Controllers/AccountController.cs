using AutoMapper;
using Common;
using Common.Http;
using Common.Paganation;
using Data;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Auth;
using Service.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _repository;

        public AccountController(IUserService userService, IAuthService authService, IUnitOfWork unitOfWork, IRepository<User> repository)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetList([FromQuery] SearchPaganationDTO<UserDTO> userDTO)
        {
            IActionResult result;
            try
            {
                result = CommonResponse(0, _userService.SearchPagination(userDTO));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                result = CommonResponse(1, Constants.Server.ErrorServer);
            }
            return result;
        }


        // POST api/<AccountController>
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin data)
        {
            if (!ModelState.IsValid || data == null || string.IsNullOrWhiteSpace(data.UserName))
            {
                return CommonResponse(1, Constants.Account.InvalidAuthInfoMsg);
            }
            try
            {
                var token = _authService.Login(data);
                return CommonResponse(0, token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return CommonResponse(1, ex.Message);
            }
        }
    }
}
