using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.BaseService
{
    public interface IBaseEditService<T, F> where T : class where F : class
    {
        Task<IActionResult> Create(F entity);
        Task<IActionResult> Update(T entity);
        Task<IActionResult> Delete(Guid id);
    }
}
