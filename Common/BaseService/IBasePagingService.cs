﻿using Common.Paganation;
using Infrastructure.EntityFramework;

namespace Common.Category
{
    public interface IBasePagingService<T,F>
    {
        public PaginatedList<T> SearchPagination(SerachPaganationDTO<F> entity);
    }
}