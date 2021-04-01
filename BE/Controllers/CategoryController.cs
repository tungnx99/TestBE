using AutoMapper;
using Common;
using Common.Http;
using Common.Paganation;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Category;
using Service.Product;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(IRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryService categoryService, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories([FromQuery] SerachPaganationDTO<CategoryDTO> serachPaganation)
        {
            IActionResult result;
            try
            {
                var products = _categoryService.SearchPagination(serachPaganation);
                result = CommonResponse(0, products);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.ToString());
                result = CommonResponse(1, ex.ToString());
            }

            return result;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IActionResult result;
            try
            {
                var products = _repository.Queryable().ToList();
                result = CommonResponse(0, products);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.ToString());
                result = CommonResponse(1, Constants.Server.ErrorServer);
            }

            return result;
        }

        [HttpPost]
        public IActionResult SaveCategory([FromForm] CategoryDTO category)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<CategoryDTO, Category>(category);
                _repository.Insert(item);
                _unitOfWork.SaveChanges();

                result = CommonResponse(0, Common.Constants.Data.InsertSuccess);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Debug.WriteLine("Error: ", ex.ToString());
                result = CommonResponse(0, Common.Constants.Server.ErrorServer);
            }

            return result;
        }

        [HttpPut]
        public IActionResult UpdateCategory([FromForm] CategoryDTO category)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<CategoryDTO, Category>(category);
                _repository.Update(item);
                _unitOfWork.SaveChanges();

                result = CommonResponse(0, Common.Constants.Data.UpdateSuccess);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Debug.WriteLine("Error: ", ex.ToString());
                result = CommonResponse(0, Common.Constants.Server.ErrorServer);
            }

            return result;
        }

        [HttpDelete]
        public IActionResult DeleteCategory([FromQuery] String id)
        {
            IActionResult result;
            try
            {
                var item = _repository.Find(Guid.Parse(id));
                _repository.Delete(item);
                _unitOfWork.SaveChanges();

                result = CommonResponse(0, Common.Constants.Data.DeleteSuccess);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                Debug.WriteLine("Error: ", ex.ToString());
                result = CommonResponse(0, Common.Constants.Server.ErrorServer);
            }

            return result;
        }
    }
}
