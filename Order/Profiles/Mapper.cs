using AutoMapper;
using Order.Entities.Models;
using Order.Entities.Dtos;

namespace Order.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //wish list
            CreateMap<CreateWishListDto, WishList>().ReverseMap();

            //cart
            CreateMap<CreateCartDto, Cart>().ReverseMap();

        }
    }
}
