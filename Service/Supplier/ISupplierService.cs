using Common.Paganation;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Supplier
{
    public interface ISupplierService : IBaseService<Domain.Entities.Supplier>
    {
        Paganation<SupplierDTO> SearchPagination(SerachPaganationDTO<Domain.Entities.Supplier> entity);
    }
}
