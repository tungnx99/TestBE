using AutoMapper;
using Common.Paganation;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Domain.Entities.Supplier> _repository;
        private readonly IMapper _mapper;

        public SupplierService(IRepository<Domain.Entities.Supplier> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public PaginatedList<SupplierDTO> SearchPagination(SerachPaganationDTO<SupplierDTO> entity)
        {
            if (entity == null)
            {
                return new PaginatedList<SupplierDTO>(null, 0, 0, 0);
            }

            var query = _repository.Queryable().Where(it => entity.Search == null ||
                (
                    (
                        (entity.Search.Id == Guid.Empty ? false : it.Id == entity.Search.Id) ||
                        it.Name.Contains(entity.Search.Name) ||
                        it.Description.Contains(entity.Search.Description)
                    )
                )
            ).OrderBy(t => t.Name);

            var data = _mapper.Map<List<Domain.Entities.Supplier>, List<SupplierDTO>>(query.ToList());
            var result = new PaginatedList<SupplierDTO>(data, data.Count, entity.PageIndex, entity.PageSize);
            result.GetPageData();

            return result;
        }
    }
}
