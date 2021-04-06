﻿using AutoMapper;
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

        public Task<IActionResult> Create(ProductDTOInsert entity)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<ProductDTOInsert, Domain.Entities.Product>(entity);
                _repositoryProduct.Insert(item);
                _unitOfWork.SaveChanges();
                result = new JsonResult(new CommonResponse<string>(0, Common.Constants.Data.InsertSuccess));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = new JsonResult(new CommonResponse<string>(1, Common.Constants.Server.ErrorServer));
            }

            return Task.FromResult(result);
        }

        public Task<IActionResult> Delete(Guid id)
        {
            IActionResult result;
            try
            {
                var item = _repositoryProduct.Find(id);
                _repositoryProduct.Delete(item);
                _unitOfWork.SaveChanges();
                result = new JsonResult(new CommonResponse<string>(0, Common.Constants.Data.DeleteSuccess));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = new JsonResult(new CommonResponse<string>(1, Common.Constants.Server.ErrorServer));
            }

            return Task.FromResult(result);
        }

        public Task<IActionResult> Update(ProductDTO entity)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<ProductDTO, Domain.Entities.Product>(entity);
                _repositoryProduct.Update(item);
                _unitOfWork.SaveChanges();
                result = new JsonResult(new CommonResponse<string>(0, Common.Constants.Data.UpdateSuccess));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = new JsonResult(new CommonResponse<string>(1, Common.Constants.Server.ErrorServer));
            }

            return Task.FromResult(result);
        }
        PaginatedList<ProductDTO> IBasePagingService<ProductDTO, SearchProductDTO>.SearchPagination(SerachPaganationDTO<SearchProductDTO> entity)
        {
            if (entity == null)
            {
                return new PaginatedList<ProductDTO>(null, 0, 0, 0);
            }

            var query = _repositoryProduct.Queryable().Where(it => entity.Search == null ||
                (
                    (entity.Search.Id == Guid.Empty ? false : it.Id == entity.Search.Id) ||
                    it.Name.Contains(entity.Search.Name) ||
                    it.Description.Contains(entity.Search.Description) ||
                    (entity.Search.Supplier == null ? false : it.Supplier.Name.Contains(entity.Search.Supplier.Name)) ||
                    (entity.Search.Category == null ? false : it.Category.Name.Contains(entity.Search.Category.Name))
                )
            ).OrderBy(t => t.Name);

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
            var pageitems = query.Skip(entity.PageIndex * entity.PageSize).Take(entity.PageSize);
            var data = _mapper.Map<List<Domain.Entities.Product>, List<ProductDTO>>(pageitems.ToList());
            var result = new PaginatedList<ProductDTO>(data, total, entity.PageIndex, entity.PageSize);

            return result;
        }
    }
}
