using AutoMapper;
using Common.Paganation;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using Service.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Domain.Entities.Product> _repositoryProduct;
        private readonly IRepository<Domain.Entities.Category> _repositoryCategory;
        private readonly IRepository<Domain.Entities.Supplier> _repositorySupplier;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Domain.Entities.Product> repositoryProduct, IMapper mapper, IRepository<Domain.Entities.Category> repositoryCategory, IRepository<Domain.Entities.Supplier> repositorySupplier)
        {
            _repositoryProduct = repositoryProduct;
            _mapper = mapper;
            _repositoryCategory = repositoryCategory;
            _repositorySupplier = repositorySupplier;
        }

        PaginatedList<ProductDTO> IBaseService<ProductDTO, SearchProductDTO>.SearchPagination(SerachPaganationDTO<SearchProductDTO> entity)
        {
            if (entity == null)
            {
                return new PaginatedList<ProductDTO>(null, 0, 0, 0);
            }

            //var query = _repository.Queryable().Where(it => entity.Search == null ||
            //    (
            //        (entity.Search.Id == Guid.Empty ? false : it.Id == entity.Search.Id) ||
            //        it.Name.Contains(entity.Search.Name) ||
            //        it.Description.Contains(entity.Search.Description)
            //    )
            //).OrderBy(t => t.Name);

            var query = (
                             from p in _repositoryProduct.Queryable()
                             join c in _repositoryCategory.Queryable() on p.CategoryId equals c.Id
                             join s in _repositorySupplier.Queryable() on p.SupplierId equals s.Id
                             where entity.Search == null ||
                                (
                                    p.Name.Contains(entity.Search.Name) ||
                                    (entity.Search.Supplier == null ? false : s.Name.Contains(entity.Search.Supplier.Name)) ||
                                    (entity.Search.Category == null ? false : c.Name.Contains(entity.Search.Category.Name))
                                )
                             select p
                         );

            var data = _mapper.Map<List<Domain.Entities.Product>, List<ProductDTO>>(query.ToList());
            var result = new PaginatedList<ProductDTO>(data, data.Count, entity.PageIndex, entity.PageSize);
            result.GetPageData();

            return result;
        }
    }
}
