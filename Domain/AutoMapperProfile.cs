using AutoMapper;
using Common.Paganation;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTOInsert, Category>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductDTOInsert>().ReverseMap();
            CreateMap<Product, ProductDTOReturn>().ReverseMap();

            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<Supplier, SupplierDTOInsert>().ReverseMap();

            CreateMap<IFormFile, File>().ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
