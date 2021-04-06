using Common.Paganation;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.BaseService;
using Service.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Supplier
{
    public interface ISupplierService : IBasePagingService<SupplierDTO,SupplierDTO>, IBaseEditService<SupplierDTO,SupplierDTOInsert>
    {
        public Task<IActionResult> GetList();
    }
}
