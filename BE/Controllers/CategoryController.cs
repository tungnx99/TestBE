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
        public IActionResult GetCategories([FromQuery] SearchPaganationDTO<CategoryDTO> serachPaganation)
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
            IActionResult result;
            try
            {
                var data = _categoryService.GetList();
                result = CommonResponse(0, data);
            }catch(Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = CommonResponse(1, Common.Constants.Server.ErrorServer);
            }
            return Task.FromResult(result);
        }

        [HttpPost]
        public Task<IActionResult> SaveCategory([FromForm] CategoryDTOInsert category)
        {
            var result = _categoryService.Create(category);
            if(!result)
            {
                return Task.FromResult(CommonResponse(1, Constants.Server.ErrorServer));
            }
            return Task.FromResult(CommonResponse(0, Constants.Data.InsertSuccess));
        }

        [HttpPut]
        public Task<IActionResult> UpdateCategory([FromForm] CategoryDTO category)
        {
            var result = _categoryService.Update(category);
            if (!result)
            {
                return Task.FromResult(CommonResponse(1, Constants.Server.ErrorServer));
            }
            return Task.FromResult(CommonResponse(0, Constants.Data.UpdateSuccess));
        }

        [HttpDelete]
        public Task<IActionResult> DeleteCategory([FromQuery] String id)
        {
            var result = _categoryService.Delete(Guid.Parse(id));
            if (!result)
            {
                return Task.FromResult(CommonResponse(1, Constants.Server.ErrorServer));
            }
            return Task.FromResult(CommonResponse(0, Constants.Data.DeleteSuccess));
        }
    }
}
