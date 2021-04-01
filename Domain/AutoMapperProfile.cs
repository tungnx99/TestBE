using AutoMapper;
using Common.Paganation;
using Domain.DTOs;
using Domain.Entities;

namespace Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SerachPaganationDTO<Category>, Paganation<Category>>().ReverseMap();
            CreateMap<SerachPaganationDTO<Category>, Paganation<CategoryDTO>>().ReverseMap();
            CreateMap<SerachPaganationDTO<Product>, Paganation<Product>>().ReverseMap();
            //CreateMap<SerachPaganationDTO<Product>, Paganation<ProductDTO>>().ReverseMap();
            CreateMap<SerachPaganationDTO<ProductDTO>, Paganation<ProductDTO>>().ReverseMap();
            CreateMap<SerachPaganationDTO<Supplier>, Paganation<Supplier>>().ReverseMap();
            CreateMap<SerachPaganationDTO<Supplier>, Paganation<SupplierDTO>>().ReverseMap();
            CreateMap<SerachPaganationDTO<CategoryDTO>, Paganation<CategoryDTO>>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Supplier, SupplierDTO>().ReverseMap();
        }
    }
}
