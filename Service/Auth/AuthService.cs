using AutoMapper;
using Common;
using Data;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service.Auth
{
    public class AuthService : IAuthService
    {
        private IRepository<Domain.Entities.User> _repository;
        private IMapper _mapper; // Remove unused code
        private IJwtManager _jwtManager;

        public AuthService(IMapper mapper, IJwtManager jwtManager, IRepository<Domain.Entities.User> repository)
        {
            _mapper = mapper;
            _jwtManager = jwtManager;
            _repository = repository;
        }
        public string Login(UserLogin data)
        {
            if (data.UserName == null)
            {
                throw new Exception(Constants.Account.InvalidAuthInfoMsg);
            }
            //if (data == null)
            //    return false;

            var account = _repository.Queryable().Any(a => a.UserName == data.UserName && a.Password == data.Password);
            if (!account)
            {
                throw new Exception(Constants.Account.InvalidAuthInfoMsg);
            }


            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, data.UserName)
            };

            // Generate JWT token
            var token = _jwtManager.GenerateToken(claims, DateTime.Now);
            return token;
        }
    }
}
