using Common.BaseService;
using Common.Category;
using Common.Paganation;
using Domain.DTOs;

namespace Service.Product
{
    public interface IProductService : IBasePagingService<ProductDTO, SearchProductDTO>, IBaseEditService<ProductDTO,ProductDTOInsert>
    {
    }
}
