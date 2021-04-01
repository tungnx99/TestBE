using Common.Paganation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IBaseService<T>
    {
        void Insert(T entity);
        void InsertRange(List<T> entities);
        void Update(T entity);
        void Delete(Guid id);

    }
}
