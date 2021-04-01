using Common.Paganation;
using Domain.DTOs;
using Service.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Product
{
    public interface IProductService : IBaseService<ProductDTO, SearchProductDTO>
    {
    }
}
