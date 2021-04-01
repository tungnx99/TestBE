using AutoMapper;
using Common;
using Common.Http;
using Common.Paganation;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ProductController : BaseController
    {
        private readonly IRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IRepository<Product> repository, IUnitOfWork unitOfWork, IProductService productService, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories([FromQuery] SerachPaganationDTO<ProductDTO> serachPaganation)
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
                result = CommonResponse(1, ex.ToString());
            }

            return result;
        }

        [HttpPost]
        public IActionResult SaveCategory([FromForm] ProductDTO dto)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<ProductDTO, Product>(dto);
                _productService.Insert(item);

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
        public IActionResult UpdateCategory([FromForm] ProductDTO category)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<ProductDTO, Product>(category);
                _productService.Update(item);

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
                _productService.Delete(Guid.Parse(id));

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
