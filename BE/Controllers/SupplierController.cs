using AutoMapper;
using Common;
using Common.Http;
using Common.Paganation;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Supplier;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _productService;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories([FromQuery] SearchPaganationDTO<SupplierDTO> serachPaganation)
        {
            IActionResult result;
            try
            {
                var products = _productService.SearchPagination(serachPaganation);
                result = CommonResponse(0, products);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.ToString());
                result = CommonResponse(1, Constants.Server.ErrorServer);
            }

            return result;
        }

        [HttpGet("all")]
        public Task<IActionResult> GetAll()
        {
            IActionResult result;
            try
            {
                var data = _productService.GetList();
                result = CommonResponse(0, data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = CommonResponse(1, Common.Constants.Server.ErrorServer);
            }
            return Task.FromResult(result);
        }

        [HttpPost]
        public Task<IActionResult> SaveCategory([FromForm] SupplierDTOInsert dto)
        {
            var result = _productService.Create(dto);
            if (!result)
            {
                return Task.FromResult(CommonResponse(1, Constants.Server.ErrorServer));
            }
            return Task.FromResult(CommonResponse(0, Constants.Data.InsertSuccess));
        }

        [HttpPut]
        public Task<IActionResult> UpdateCategory([FromForm] SupplierDTO category)
        {
            var result = _productService.Update(category);
            if (!result)
            {
                return Task.FromResult(CommonResponse(1, Constants.Server.ErrorServer));
            }
            return Task.FromResult(CommonResponse(0, Constants.Data.UpdateSuccess));
        }

        [HttpDelete]
        public Task<IActionResult> DeleteCategory([FromQuery] String id)
        {
            var result = _productService.Delete(Guid.Parse(id));
            if (!result)
            {
                return Task.FromResult(CommonResponse(1, Constants.Server.ErrorServer));
            }
            return Task.FromResult(CommonResponse(0, Constants.Data.DeleteSuccess));
        }
    }
}
