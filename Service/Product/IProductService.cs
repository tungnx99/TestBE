using Common.Paganation;
using Domain.DTOs;
using Service.BaseService;
using Service.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Product
{
    public interface IProductService : IBasePagingService<ProductDTO, SearchProductDTO>, IBaseEditService<ProductDTO,ProductDTOInsert>
    {
    }
}
