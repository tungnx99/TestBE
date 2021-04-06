using AutoMapper;
using Common.Category;
using Common.Http;
using Common.Paganation;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Service.Category;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Domain.Entities.Product> _repositoryProduct;
        private readonly IRepository<Domain.Entities.Category> _repositoryCategory;
        private readonly IRepository<Domain.Entities.Supplier> _repositorySupplier;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Domain.Entities.Product> repositoryProduct, IMapper mapper, IRepository<Domain.Entities.Category> repositoryCategory, IRepository<Domain.Entities.Supplier> repositorySupplier, IUnitOfWork unitOfWork)
        {
            _repositoryProduct = repositoryProduct;
            _mapper = mapper;
            _repositoryCategory = repositoryCategory;
            _repositorySupplier = repositorySupplier;
            _unitOfWork = unitOfWork;
        }

        public Boolean Create(ProductDTOInsert entity)
        {
            Boolean result;
            try
            {
                var item = _mapper.Map<ProductDTOInsert, Domain.Entities.Product>(entity);
                _repositoryProduct.Insert(item);
                _unitOfWork.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = false;
            }

            return result;
        }

        public Boolean Delete(Guid id)
        {
            Boolean result;
            try
            {
                var item = _repositoryProduct.Find(id);
                _repositoryProduct.Delete(item);
                _unitOfWork.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = false;
            }

            return result;
        }

        public ProductDTOReturn GetByID(Guid id)
        {
            ProductDTOReturn result;
            try
            {
                var product = _repositoryProduct.Queryable().Where(it => it.Id == id).Select(
                    it =>
                    new ProductDTOReturn()
                    {
                        Id = it.Id,
                        Description = it.Description,
                        CategoryName = it.Category.Name,
                        Name = it.Name,
                        SupplierName = it.Supplier.Name,
                        CategoryId = it.CategoryId.GetValueOrDefault(),
                        SupplierId = it.SupplierId.GetValueOrDefault()
                    }
                );
                result = product.FirstOrDefault();
            }catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        public Boolean Update(ProductDTO entity)
        {
            Boolean result;
            try
            {
                var item = _mapper.Map<ProductDTO, Domain.Entities.Product>(entity);
                _repositoryProduct.Update(item);
                _unitOfWork.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = false;
            }

            return result;
        }
        PaginatedList<ProductDTOReturn> IBasePagingService<ProductDTOReturn, SearchProductDTO>.SearchPagination(SerachPaganationDTO<SearchProductDTO> entity)
        {
            if (entity == null)
            {
                return new PaginatedList<ProductDTOReturn>(null, 0, 0, 0);
            }

            var query = _repositoryProduct.Queryable().Where(it => entity.Search == null ||
                (
                    (entity.Search.Id == Guid.Empty ? false : it.Id == entity.Search.Id) ||
                    it.Name.Contains(entity.Search.Name) ||
                    it.Description.Contains(entity.Search.Description) ||
                    (entity.Search.Supplier == null ? false : it.Supplier.Name.Contains(entity.Search.Supplier.Name)) ||
                    (entity.Search.Category == null ? false : it.Category.Name.Contains(entity.Search.Category.Name))
                )
            ).Select(it => new ProductDTOReturn()
            {
                Id = it.Id,
                Description = it.Description,
                CategoryName = it.Category.Name,
                Name = it.Name,
                SupplierName = it.Supplier.Name,
                CategoryId = it.CategoryId.GetValueOrDefault(),
                SupplierId = it.SupplierId.GetValueOrDefault()
            })
            .OrderBy(t => t.Name);

            //var query = (
            //                 from p in _repositoryProduct.Queryable()
            //                 join c in _repositoryCategory.Queryable() on p.CategoryId equals c.Id
            //                 join s in _repositorySupplier.Queryable() on p.SupplierId equals s.Id
            //                 where entity.Search == null ||
            //                    (
            //                        p.Name.Contains(entity.Search.Name) ||
            //                        (entity.Search.Supplier == null ? false : s.Name.Contains(entity.Search.Supplier.Name)) ||
            //                        (entity.Search.Category == null ? false : c.Name.Contains(entity.Search.Category.Name))
            //                    )
            //                 select p
            //             );

            //clean
            var total = query.Count();
            var pageitems = query.Skip((entity.PageIndex - 1) * entity.PageSize).Take(entity.PageSize);
            //var data = _mapper.Map<List<Domain.Entities.Product>, List<ProductDTOReturn>>(pageitems.ToList());
            var data = pageitems.ToList();
            var result = new PaginatedList<ProductDTOReturn>(data, total, entity.PageIndex, entity.PageSize);

            return result;
        }
    }
}
