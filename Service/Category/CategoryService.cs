using AutoMapper;
using Common;
using Common.Http;
using Common.Paganation;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Domain.Entities.Category> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IRepository<Domain.Entities.Category> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public PaginatedList<CategoryDTO> SearchPagination(SerachPaganationDTO<CategoryDTO> entity)
        {
            //List<Domain.Entities.Category> categories = new List<Domain.Entities.Category>();
            //List<Domain.Entities.Product> products = new List<Domain.Entities.Product>();
            //List<Domain.Entities.Supplier> suppliers = new List<Domain.Entities.Supplier>();

            //for(int i = 0; i < 100; i++)
            //{
            //    categories.Add(new Domain.Entities.Category()
            //    {
            //        Description = "aaa",
            //        Id = Guid.NewGuid(),
            //        Name = "Category" + i,
            //    });
            //    suppliers.Add(new Domain.Entities.Supplier()
            //    {
            //        Description = "bbb",
            //        Id = Guid.NewGuid(),
            //        Name = "Supplier" + i,
            //    });
            //}

            //for(int i= 0; i< 1000; i++)
            //{
            //    products.Add(new Domain.Entities.Product()
            //    {
            //        CategoryId = categories[new Random().Next(0, 99)].Id,
            //        Description = "ccc",
            //        Id = Guid.NewGuid(),
            //        Name = "Product" + i,
            //        SupplierId = suppliers[new Random().Next(0, 99)].Id,
            //    });
            //}

            //_repository.InsertRange(categories);
            //_repositorys.InsertRange(suppliers);
            //_repositoryp.InsertRange(products);
            //_unitOfWork.SaveChanges();

            if (entity == null)
            {
                return new PaginatedList<CategoryDTO>(null, 0, 0, 0);
            }

            var query = _repository.Queryable().Where(it => it.IsDeleted == false && 
                (
                    entity.Search == null ||
                        (
                            (entity.Search.Id == Guid.Empty ? false : it.Id == entity.Search.Id) ||
                            it.Name.Contains(entity.Search.Name) ||
                            it.Description.Contains(entity.Search.Description)
                        )
                )
            ).OrderBy(t => t.Name);

            //clean
            var data = _mapper.Map<List<Domain.Entities.Category>, List<CategoryDTO>>(query.ToList());
            var result = new PaginatedList<CategoryDTO>(data, data.Count, entity.PageIndex, entity.PageSize);
            result.GetPageData();

            return result;
        }

        public Task<IActionResult> Create(CategoryDTOInsert entity)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<CategoryDTOInsert, Domain.Entities.Category>(entity);
                _repository.Insert(item);
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
                var item = _repository.Find(id);
                item.IsDeleted = true;
                _repository.Update(item);
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

        public Task<IActionResult> Update(CategoryDTO entity)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<CategoryDTO, Domain.Entities.Category>(entity);
                _repository.Update(item);
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

        public Task<IActionResult> GetList()
        {
            IActionResult result;
            try
            {
                var products = _repository.Queryable().Where(it => it.IsDeleted == false).ToList();
                result = new JsonResult(new CommonResponse<List<Domain.Entities.Category>>(0, products));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.ToString());
                result = new JsonResult(new CommonResponse<String>(1, Constants.Server.ErrorServer));
            }

            return Task.FromResult(result);
        }
    }
}
