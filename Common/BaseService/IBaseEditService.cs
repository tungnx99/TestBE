using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BaseService
{
    public interface IBaseEditService<T, F> where T : class where F : class
    {
        Boolean Create(F entity);
        Boolean Update(T entity);
        Boolean Delete(Guid id);
    }
}
