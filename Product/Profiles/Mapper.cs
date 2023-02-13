using AutoMapper;
using Product.Entities.Models;
using Product.Entities.Dtos;
using System;

namespace Product.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //create product
            CreateMap<CreateProductDto, ProductDetail>().ForMember(
              dest => dest.CategoryId,
              opt => opt.MapFrom(src => (Guid.Parse(src.Type)))).ReverseMap();
            CreateMap<UpdateProductDto, ProductDetail>().ForMember(
              dest => dest.CategoryId,
              opt => opt.MapFrom(src => (Guid.Parse(src.Type)))).ReverseMap();
            CreateMap<ProductDetail, ProductDto>();
            CreateMap<ProductDto, ResultProductDto>().ReverseMap();
            CreateMap<ProductDetail,ResultProductDto>().ReverseMap();

        }
    }
}
