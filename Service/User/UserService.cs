using AutoMapper;
using Common.Paganation;
using Data;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.EntityFramework;
using System.Collections.Generic;
using System.Linq;
namespace Service.Users
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private readonly IRepository<User> dataRepository;

        public UserService(IMapper mapper, IRepository<User> dataRepository)
        {
            _mapper = mapper;
            this.dataRepository = dataRepository;
        }

        public PaginatedList<UserDTO> SearchPagination(SearchPaganationDTO<UserDTO> entity)
        {
            if (entity == null)
            {
                return new PaginatedList<UserDTO>(null, 0, 0, 0);
            }

            //using (ShopDbContext shopDbContext = this.shhopDbContext)
            //{


            var query = dataRepository.Queryable()
                .Where(it => entity.Search == null || it.UserName.Contains(entity.Search.UserName))
                .OrderBy(it => it.Role)
                .ThenBy(it => it.UserName);

            var count = query.Count();
            var userDTOs = _mapper.Map<List<User>,List<UserDTO>>(query.Skip((entity.PageIndex - 1) * entity.PageSize).Take(entity.PageSize).ToList());


            var result = new PaginatedList<UserDTO>(userDTOs, count, entity.PageIndex, entity.PageSize);

            return result;
        }
        //}
    }
}
