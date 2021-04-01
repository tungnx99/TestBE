using AutoMapper;
using Common.Paganation;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Domain.Entities.Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Domain.Entities.Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public PaginatedList<CategoryDTO> SearchPagination(SerachPaganationDTO<CategoryDTO> entity)
        {
            if (entity == null)
            {
                return new PaginatedList<CategoryDTO>(null, 0, 0, 0);
            }

            var query = _repository.Queryable().Where(it => entity.Search == null ||
                (
                    (entity.Search.Id == Guid.Empty ? false : it.Id == entity.Search.Id) ||
                    it.Name.Contains(entity.Search.Name) ||
                    it.Description.Contains(entity.Search.Description)
                )
            ).OrderBy(t => t.Name);

            var data = _mapper.Map<List<Domain.Entities.Category>, List<CategoryDTO>>(query.ToList());
            var result = new PaginatedList<CategoryDTO>(data, data.Count, entity.PageIndex, entity.PageSize);
            result.GetPageData();

            return result;
        }
    }
}
