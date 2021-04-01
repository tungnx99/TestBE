using Common.Paganation;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Product
{
    public interface IProductService : IBaseService<Domain.Entities.Product>
    {
        Paganation<ProductDTO> SearchPagination(SerachPaganationDTO<ProductDTO> entity);
    }
}
