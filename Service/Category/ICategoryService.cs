using Common.Paganation;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Category
{
    public interface ICategoryService : IBaseService<Domain.Entities.Category>
    {
        Paganation<CategoryDTO> SearchPagination(SerachPaganationDTO<CategoryDTO> entity);
    }
}
