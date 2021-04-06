using Common.Paganation;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.BaseService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Category
{
    public interface ICategoryService : IBasePagingService<CategoryDTO,CategoryDTO>, IBaseEditService<CategoryDTO, CategoryDTOInsert>
    {
        Task<IActionResult> GetList();
    }
}
