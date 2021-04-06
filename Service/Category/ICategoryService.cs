using Common.BaseService;
using Common.Category;
using Common.Paganation;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Category
{
    public interface ICategoryService : IBasePagingService<CategoryDTO,CategoryDTO>, IBaseEditService<CategoryDTO, CategoryDTOInsert>
    {
        List<CategoryDTO> GetList();
    }
}
