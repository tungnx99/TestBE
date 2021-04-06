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
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
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
        public Task<IActionResult> GetAll()
        {
            var result = _categoryService.GetList();
            return result;
        }

        [HttpPost]
        public Task<IActionResult> SaveCategory([FromForm] CategoryDTOInsert category)
        {
            var result = _categoryService.Create(category);
            return result;
        }

        [HttpPut]
        public Task<IActionResult> UpdateCategory([FromForm] CategoryDTO category)
        {
            var result = _categoryService.Update(category);
            return result;
        }

        [HttpDelete]
        public Task<IActionResult> DeleteCategory([FromQuery] String id)
        {
            var result = _categoryService.Delete(Guid.Parse(id));
            return result;
        }
    }
}
