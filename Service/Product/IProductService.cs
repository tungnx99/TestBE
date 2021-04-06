using Common.BaseService;
using Common.Category;
using Common.Paganation;
using Domain.DTOs;
using System;

namespace Service.Product
{
    public interface IProductService : IBasePagingService<ProductDTOReturn, SearchProductDTO>, IBaseEditService<ProductDTO,ProductDTOInsert>
    {
        ProductDTOReturn GetByID(Guid id);
    }
}
